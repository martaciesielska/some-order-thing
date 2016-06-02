namespace SomeOrderThing
{
    using System.Threading;

    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder orderHandler;
        private readonly string name;
        private readonly int sleepTime;

        public Cook(IHandleOrder orderHandler, string name, int sleepTime)
        {
            this.orderHandler = orderHandler;
            this.sleepTime = sleepTime;
            this.name = name;
        }

        public void Handle(TableOrder order)
        {
            Thread.Sleep(this.sleepTime);

            var tableOrder = order.Copy();

            tableOrder.Ingredients = "KFC chicken";
            tableOrder.CookName = this.name;

            this.orderHandler.Handle(tableOrder);
        }
    }
}
