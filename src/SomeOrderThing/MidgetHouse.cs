namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using Messages.Events;

    public class MidgetHouse : IHandle<OrderPlaced>
    {
        private readonly Dictionary<Guid, Midget> midgetMappings = new Dictionary<Guid, Midget>();
        private readonly TopicBasedPubSub publisher;

        public MidgetHouse(TopicBasedPubSub publisher) // refactor later potentially
        {
            this.publisher = publisher;
        }

        public void Handle(OrderPlaced order)
        {
            var midget = this.ReleaseTheMidget();
            this.midgetMappings.Add(order.CorrelationId, midget);
            this.publisher.SubscribeByCorrelationId(order.CorrelationId, midget);
        }

        public Midget ReleaseTheMidget()
        {
            return new Midget(this.publisher);
        }
    }
}
