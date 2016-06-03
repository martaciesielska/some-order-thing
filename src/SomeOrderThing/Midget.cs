namespace SomeOrderThing
{
    using System;
    using Messages;
    using Messages.Commands;
    using Messages.Events;

    public class Midget : 
        IHandle<OrderPlaced>, 
        IHandle<OrderCooked>,
        IHandle<OrderPriced>,
        IHandle<OrderPaid>
    {
        private readonly IPublisher publisher;

        public Midget(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(OrderPaid message)
        {
            this.publisher.Publish(new PrintOrder(message) { Order = message.Order });
        }

        public void Handle(OrderPriced message)
        {
            this.publisher.Publish(new TakePayment(message) { Order = message.Order });
        }

        public void Handle(OrderCooked message)
        {
            this.publisher.Publish(new PriceOrder(message) { Order = message.Order });
        }

        public void Handle(OrderPlaced message)
        {
            this.publisher.Publish(new CookFood(message) { Order = message.Order });
        }
    }
}
