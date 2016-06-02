
namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;

    public class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<string, IHandleOrder> handlers 
            = new Dictionary<string, IHandleOrder>();

        public void Publish(string topic, TableOrder order)
        {
            if (this.handlers.ContainsKey(topic))
            {
                this.handlers[topic].Handle(order);
            }
        }

        public void Subscribe(string topic, IHandleOrder handler)
        {
            this.handlers.Add(topic, handler);
        }
    }
}
