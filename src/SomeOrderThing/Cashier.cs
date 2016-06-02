namespace SomeOrderThing
{
    public class Cashier : IHandleOrder
    {
        private readonly IPublisher publisher;
        private readonly string topic;

        public Cashier(IPublisher publisher, string topic)
        {
            this.publisher = publisher;
            this.topic = topic;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();
            tableOrder.Paid = true;
            this.publisher.Publish(this.topic, tableOrder);
        }
    }
}
