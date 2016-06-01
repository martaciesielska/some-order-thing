namespace SomeOrderThing
{
    using System.Linq;

    public class AssistantManager : IHandleOrder
    {
        private readonly IHandleOrder orderHandler;

        public AssistantManager(IHandleOrder orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();

            tableOrder.Tax = 12.4m;
            tableOrder.Total = tableOrder.LineItems.Sum(lineItem => lineItem.Quantity * lineItem.Price) + tableOrder.Tax;

            orderHandler.Handle(tableOrder);
        }
    }
}
