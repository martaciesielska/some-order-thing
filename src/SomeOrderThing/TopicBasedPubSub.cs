
namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;

    public class TopicBasedPubSub : IPublisher
    {
        private readonly Dictionary<string, IHandleOrder> handlers;

        public void Publish(string topic, TableOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
