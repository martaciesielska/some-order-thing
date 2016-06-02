namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<IStartable>();
            var random = new Random();

            var printer = new PrintingHandler();
            var cashier = new TaskThreadedHandler(new Cashier(printer));
            var assMan = new TaskThreadedHandler(new AssistantManager(cashier));
            var cooks = new[] 
            {
                new TaskThreadedHandler(new Cook(assMan, "Guybrush Threepwood", random.Next(0, 5000))),
                new TaskThreadedHandler(new Cook(assMan, "Elaine Marley", random.Next(0, 5000))),
                new TaskThreadedHandler(new Cook(assMan, "Zombie Pirate LeChuck", random.Next(0, 5000)))
            };
            var multiplexer = new RoundRobinDispatcher(cooks);
            var waiter = new Waiter(multiplexer);

            list.Add(cashier);
            list.Add(assMan);
            list.AddRange(cooks);

            list.ForEach(item => item.Start());

            for (var i = 0; i < 10; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }

            Console.ReadLine();

            list.ForEach(item => item.Start());
        }
    }
}
