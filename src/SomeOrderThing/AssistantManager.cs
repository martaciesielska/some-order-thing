namespace SomeOrderThing
{
    using System.Linq;
    using Messages;

    public class AssistantManager : IHandle<OrderCooked>
    {
        private readonly IPublisher publisher;
        private readonly string topic;

        public AssistantManager(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(OrderCooked orderCooked)
        {
            var tableOrder = orderCooked.Order.Copy();

            tableOrder.Tax = 12.4m;
            tableOrder.Total = tableOrder.LineItems.Sum(lineItem => lineItem.Quantity * lineItem.Price) + tableOrder.Tax;

            this.publisher.Publish(new OrderPriced() { Order = tableOrder });
        }
    }
}
