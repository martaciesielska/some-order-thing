namespace SomeOrderThing
{
    using System.Linq;

    public class AssistantManager : IHandleOrder
    {
        private readonly IPublisher publisher;

        public AssistantManager(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();

            tableOrder.Tax = 12.4m;
            tableOrder.Total = tableOrder.LineItems.Sum(lineItem => lineItem.Quantity * lineItem.Price) + tableOrder.Tax;

            this.publisher.Publish("take payment", tableOrder);
        }
    }
}
