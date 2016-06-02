namespace SomeOrderThing
{
    using System.Threading;
    using Messages;

    public class Cook : IHandle<OrderPlaced>
    {
        private readonly IPublisher publisher;
        private readonly string name;
        private readonly int sleepTime;

        public Cook(IPublisher publisher, string name, int sleepTime)
        {
            this.publisher = publisher;
            this.sleepTime = sleepTime;
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            Thread.Sleep(this.sleepTime);

            var tableOrder = orderPlaced.Order.Copy();

            tableOrder.Ingredients = "KFC chicken";
            tableOrder.CookName = this.name;

            this.publisher.Publish(new OrderCooked() { Order = tableOrder });
        }
    }
}
