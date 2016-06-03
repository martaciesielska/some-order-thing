namespace SomeOrderThing
{
    using System.Threading;
    using Messages.Events;
    using Messages.Commands;

    public class Cook : IHandle<CookFood>
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

        public void Handle(CookFood command)
        {
            Thread.Sleep(this.sleepTime);

            var tableOrder = command.Order.Copy();

            tableOrder.Ingredients = "KFC chicken";
            tableOrder.CookName = this.name;

            this.publisher.Publish(new OrderCooked() { Order = tableOrder });
        }
    }
}
