namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using Messages.Events;
    using Messages;

    public class MidgetHouse : IHandle<OrderPlaced>
    {
        private readonly IDictionary<Guid, IHandle<IEvent>> midgetMappings 
            = new Dictionary<Guid, IHandle<IEvent>>();
        private readonly IBus publisher;

        public MidgetHouse(IBus publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(OrderPlaced @event)
        {
            var midget = this.ReleaseTheMidget(@event.Order.IsDodgy);

            midget.Finished += Midget_Finished;

            this.midgetMappings.Add(
                @event.CorrelationId,
                midget);

            this.publisher.SubscribeByCorrelationId<IEvent>(@event.CorrelationId, midget);

            midget.Handle(@event);
        }

        private void Midget_Finished(object sender, EventArgs e)
        {
            var midgetArgs = e as MidgetEventArgs;
            var midget = this.midgetMappings[midgetArgs.CorrelationId];

            this.publisher.UnsubscribeByCorrelationId(midgetArgs.CorrelationId, midget);
            this.midgetMappings.Remove(midgetArgs.CorrelationId);
        }

        public IMidget ReleaseTheMidget(bool isDodgy)
        {
            return isDodgy 
                ? (IMidget)new ZimbabwianMidget(this.publisher)
                : new LithuanianMidget(this.publisher);
        }
    }
}
