namespace SomeOrderThing
{
    using System.Threading;

    public class Cook : IHandleOrder
    {
        private readonly IPublisher publisher;
        private readonly string name;
        private readonly int sleepTime;
        private readonly string topic;

        public Cook(IPublisher publisher, string topic, string name, int sleepTime)
        {
            this.publisher = publisher;
            this.sleepTime = sleepTime;
            this.name = name;
            this.topic = topic;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public void Handle(TableOrder order)
        {
            Thread.Sleep(this.sleepTime);

            var tableOrder = order.Copy();

            tableOrder.Ingredients = "KFC chicken";
            tableOrder.CookName = this.name;

            this.publisher.Publish(this.topic, tableOrder);
        }
    }
}
