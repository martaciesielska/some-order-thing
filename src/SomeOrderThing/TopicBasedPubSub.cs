namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;

    public class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<string, dynamic> handlers 
            = new Dictionary<string, dynamic>();

        public void Publish<T>(T message)
        {
            var topic = message.GetType().Name;
            if (this.handlers.ContainsKey(topic))
            {
                this.handlers[topic].Handle(message);
            }
        }

        public void Subscribe<T>(IHandle<T> handler)
        {
            this.handlers.Add(typeof(T).Name, handler);
        }
    }
}
