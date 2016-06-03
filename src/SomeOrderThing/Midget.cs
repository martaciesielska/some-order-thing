namespace SomeOrderThing
{
    using System;
    using Messages;
    using Messages.Commands;
    using Messages.Events;

    public class Midget : IHandle<OrderPlaced>
    {
        private readonly IPublisher publisher;

        public Midget(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(OrderPlaced message)
        {
            this.publisher.Publish(new CookFood(message) { Order = message.Order });
        }
    }
}
