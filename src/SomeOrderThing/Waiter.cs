namespace SomeOrderThing
{
    public class Waiter
    {
        private readonly IHandleOrder orderHandler;

        public Waiter(IHandleOrder orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();

            tableOrder.TableNumber = 17;
            tableOrder.LineItems.Add(new TableOrder.LineItem { Quantity = 1, Item = "KFC please", Price = 4m });

            this.orderHandler.Handle(tableOrder);
        }
    }
}
