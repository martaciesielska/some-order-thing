namespace SomeOrderThing
{
    using System;
    using Messages;
    using Messages.Commands;
    using Messages.Events;

    public class LithuanianMidget : IHandle<IEvent>, IMidget
    {
        private readonly IPublisher publisher;
        public event EventHandler Finished;

        public LithuanianMidget(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(IEvent @event)
        {
            this.HandleInternal((dynamic)@event);
        }

        private void HandleInternal(OrderPaid message)
        {
            this.Finished.Invoke(this, new MidgetEventArgs() { CorrelationId = message.CorrelationId });
            this.publisher.Publish(new PrintOrder(message) { Order = message.Order });
        }

        private void HandleInternal(OrderPriced message)
        {
            this.publisher.Publish(new TakePayment(message) { Order = message.Order });
        }

        private void HandleInternal(OrderCooked message)
        {
            this.publisher.Publish(new PriceOrder(message) { Order = message.Order });
        }

        private void HandleInternal(OrderPlaced message)
        {
            this.publisher.Publish(new CookFood(message) { Order = message.Order });
        }
    }
}
