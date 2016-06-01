using System;

namespace SomeOrderThing
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new TableOrder(Guid.NewGuid())
            {
                TableNumber = 17,
            };

            var waiter = new Waiter();
            waiter.Handle(order);
        }
    }
}
