namespace SomeOrderThing
{
    using Messages;

    public class Cashier : IHandle<OrderPriced>
    {
        private readonly IPublisher publisher;

        public Cashier(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(OrderPriced orderPriced)
        {
            var tableOrder = orderPriced.Order.Copy();
            tableOrder.Paid = true;
            this.publisher.Publish(new OrderPaid() { Order = tableOrder });
        }
    }
}
