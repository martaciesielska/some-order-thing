namespace SomeOrderThing
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var order = new TableOrder(Guid.NewGuid())
            {
                TableNumber = 17,
            };

            order.LineItems.Add(new TableOrder.LineItem { Quantity = 1, Item = "KFC please", Price = 4m });

            var cook = new Cook(new DummyHandler());
            var waiter = new Waiter(cook);
            waiter.Handle(order);
        }
    }
}
