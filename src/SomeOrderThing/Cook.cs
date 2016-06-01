namespace SomeOrderThing
{
    using System.Threading;

    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder orderHandler;

        public Cook(IHandleOrder orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        public void Handle(TableOrder order)
        {
            Thread.Sleep(2000);

            order.Ingredients = "KFC chicken";

            this.orderHandler.Handle(order);
        }
    }
}
