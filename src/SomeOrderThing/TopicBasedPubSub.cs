namespace SomeOrderThing
{
    using Messages;
    using System;
    using System.Collections.Generic;

    public class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<string, Action<object>> handlers 
            = new Dictionary<string, Action<object>>();

        public void Publish(IMessage message)
        {
            var topic = message.GetType().Name;
            if (this.handlers.ContainsKey(topic))
            {
                this.handlers[topic](message);
            }
        }

        public void Subscribe<T>(IHandle<T> handler)
        {
            this.handlers.Add(typeof(T).Name, message => handler.Handle((T)message));
        }
    }
}
