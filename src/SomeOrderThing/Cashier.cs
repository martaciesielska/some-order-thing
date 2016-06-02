namespace SomeOrderThing
{
    public class Cashier : IHandleOrder
    {
        private readonly IPublisher publisher;

        public Cashier(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();
            tableOrder.Paid = true;
            this.publisher.Publish("print order", tableOrder);
        }
    }
}
