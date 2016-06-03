namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using Messages.Events;
    using Messages;

    public class MidgetHouse : IHandle<OrderPlaced>
    {
        private readonly IDictionary<Guid, Midget> midgetMappings 
            = new Dictionary<Guid, Midget>();
        private readonly TopicBasedPubSub publisher;

        public MidgetHouse(TopicBasedPubSub publisher) // refactor later potentially
        {
            this.publisher = publisher;
        }

        public void Handle(OrderPlaced order)
        {
            var midget = this.ReleaseTheMidget();

            midget.Finished += Midget_Finished;

            this.midgetMappings.Add(
                order.CorrelationId,
                midget);

            this.publisher.SubscribeByCorrelationId<IEvent>(order.CorrelationId, midget);

            midget.Handle(order);
        }

        private void Midget_Finished(object sender, EventArgs e)
        {
            var midgetArgs = e as MidgetEventArgs;
            this.midgetMappings.Remove(midgetArgs.CorrelationId);
        }

        public Midget ReleaseTheMidget()
        {
            return new Midget(this.publisher);
        }
    }
}
