namespace SomeOrderThing
{
    using System;
    using Messages;
    using Messages.Commands;
    using Messages.Events;

    public class Midget : 
        IHandle<IEvent>
    {
        private readonly IPublisher publisher;
        public event EventHandler Finished;

        public Midget(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(IEvent @event)
        {
            this.HandleInternal((dynamic)@event);
        }

        private void HandleInternal(OrderPaid message)
        {
            this.publisher.Publish(new PrintOrder(message) { Order = message.Order });
            this.Finished.Invoke(this, new MidgetEventArgs() { CorrelationId = message.CorrelationId });
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

    public class MidgetEventArgs : EventArgs
    {
        public Guid CorrelationId { get; set; }
    }
}
