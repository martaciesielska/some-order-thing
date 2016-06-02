namespace SomeOrderThing
{
    using System.Linq;

    public class AssistantManager : IHandleOrder
    {
        private readonly IPublisher publisher;
        private readonly string topic;

        public AssistantManager(IPublisher publisher, string topic)
        {
            this.publisher = publisher;
            this.topic = topic;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();

            tableOrder.Tax = 12.4m;
            tableOrder.Total = tableOrder.LineItems.Sum(lineItem => lineItem.Quantity * lineItem.Price) + tableOrder.Tax;

            this.publisher.Publish(this.topic, tableOrder);
        }
    }
}
