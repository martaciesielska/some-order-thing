namespace SomeOrderThing
{
    using System.Collections.Generic;
    using System.Collections.Concurrent;
    using Messages;
    using Messages.Events;
    using System;
    public class TopicBasedPubSub : IPublisher
    {
        private readonly ConcurrentDictionary<string, List<object>> handlers
            = new ConcurrentDictionary<string, List<object>>();

        public void Publish<T>(T message) where T : IMessage
        {
            var topic = message.GetType().Name;
            var correlationId = message.CorrelationId.ToString("N");

            this.PublishInternal(topic, message);
            this.PublishInternal(correlationId, message);
        }

        private void PublishInternal<T>(string topic, T message) where T : IMessage
        {
            List<object> handlersOfT;

            if (this.handlers.TryGetValue(topic, out handlersOfT))
            {
                foreach (var handler in handlersOfT)
                {
                    ((dynamic)handler).Handle(message);
                }
            }
        }

        public void SubscribeByType<T>(IHandle<T> handler)
        {
            this.Subscribe(typeof(T).Name, handler);
        }

        public void SubscribeByCorrelationId<T>(Guid correlationId, IHandle<T> handler)
        {
            this.Subscribe(correlationId.ToString("N"), handler);
        }

        public void Subscribe<T>(string topic, IHandle<T> handler)
        {
            bool success;
            do
            {
                var handlersOfT = this.handlers.GetOrAdd(topic, t => new List<object>());

                var list = new List<object>(handlersOfT);
                list.Add(handler);

                success = this.handlers.TryUpdate(topic, list, handlersOfT);
            }
            while (!success);
        }

        public void UnsubscribeByType<T>(IHandle<T> handler)
        {
            this.Unsubscribe(typeof(T).Name, handler);
        }
   
        public void UnsubscribeByCorrelationId<T>(Guid correlationId, IHandle<T> handler)
        {
            this.Unsubscribe(correlationId.ToString("N"), handler);
        }

        public void Unsubscribe<T>(string topic, IHandle<T> handler)
        {
            bool success;
            do
            {
                List<object> handlersOfT;
                if (!this.handlers.TryGetValue(topic, out handlersOfT))
                {
                    return;
                }

                var list = new List<object>(handlersOfT);
                list.Remove(handler);

                success = this.handlers.TryUpdate(topic, list, handlersOfT);
            }
            while (!success);
        }
    }
}
