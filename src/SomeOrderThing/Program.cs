namespace SomeOrderThing
{
    using System;
    using System.Diagnostics;
    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new PrintingHandler();
            var cashier = new TaskThreadedHandler(new Cashier(printer));
            var assMan = new TaskThreadedHandler(new AssistantManager(cashier));
            var cooks = new[] { new TaskThreadedHandler(new Cook(assMan)), new TaskThreadedHandler(new Cook(assMan)), new TaskThreadedHandler(new Cook(assMan)) };
            var multiplexer = new RoundRobinDispatcher(cooks);
            var waiter = new Waiter(multiplexer);

            foreach (var cook in cooks)
            {
                cook.Start();
            }
            cashier.Start();
            assMan.Start();

            for (var i = 0; i < 10; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }

            Console.ReadLine();

            foreach (var cook in cooks)
            {
                cook.Dispose();
            }
            cashier.Dispose();
            assMan.Dispose();
        }
    }
}
