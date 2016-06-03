namespace SomeOrderThing
{
    using Messages.Events;
    using System;
    public class Waiter
    {
        private readonly IPublisher publisher;

        public Waiter(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(TableOrder order)
        {
            var tableOrder = order.Copy();
            var random = new Random();

            tableOrder.TableNumber = 17;
            tableOrder.LineItems.Add(
                new TableOrder.LineItem
                {
                    Quantity = 1,
                    Item = "KFC please",
                    Price = 4m
                });

            tableOrder.IsDodgy = random.Next(2) == 1;

            this.publisher.Publish(new OrderPlaced { Order = tableOrder, CorrelationId = order.Id });
        }
    }
}
