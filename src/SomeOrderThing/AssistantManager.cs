namespace SomeOrderThing
{
    using System.Linq;
    using Messages.Events;
    using Messages.Commands;

    public class AssistantManager : IHandle<PriceOrder>
    {
        private readonly IPublisher publisher;

        public AssistantManager(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(PriceOrder command)
        {
            var tableOrder = command.Order.Copy();

            tableOrder.Tax = 12.4m;
            tableOrder.Total = tableOrder.LineItems.Sum(lineItem => lineItem.Quantity * lineItem.Price) + tableOrder.Tax;

            this.publisher.Publish(new OrderPriced(command) { Order = tableOrder });
        }
    }
}
