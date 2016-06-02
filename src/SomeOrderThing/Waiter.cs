namespace SomeOrderThing
{
    public class Waiter
    {
        private readonly IPublisher publisher;
        private readonly string topic;

        public Waiter(IPublisher publisher, string topic)
        {
            this.publisher = publisher;
            this.topic = topic;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();

            tableOrder.TableNumber = 17;
            tableOrder.LineItems.Add(new TableOrder.LineItem { Quantity = 1, Item = "KFC please", Price = 4m });

            this.publisher.Publish(this.topic, tableOrder);
        }
    }
}
