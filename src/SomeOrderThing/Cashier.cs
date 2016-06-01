namespace SomeOrderThing
{
    public class Cashier : IHandleOrder
    {
        private readonly IHandleOrder orderHandler;
        public Cashier(IHandleOrder orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();
            tableOrder.Paid = true;
            this.orderHandler.Handle(tableOrder);
        }
    }
}
