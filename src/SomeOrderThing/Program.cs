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

            var cook = new Cook(new DummyHandler());
            var waiter = new Waiter(cook);
            waiter.Handle(order);
        }
    }
}
