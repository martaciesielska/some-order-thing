namespace SomeOrderThing
{
    using System;
    using Messages;
    using Messages.Commands;
    using Messages.Events;

    public class Midget : 
        IHandle<IMessage>
    {
        private readonly IPublisher publisher;

        public Midget(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(IMessage order)
        {
            this.HandleInternal((dynamic)order);
        }

        private void HandleInternal(OrderPaid message)
        {
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
