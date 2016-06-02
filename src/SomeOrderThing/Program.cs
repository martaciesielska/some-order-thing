namespace SomeOrderThing
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<TaskThreadedHandler>();
            var random = new Random();

            var printer = new PrintingHandler();
            var cashier = new TaskThreadedHandler(new Cashier(printer), "cashier");
            var assMan = new TaskThreadedHandler(new AssistantManager(cashier), "assMan");
            var cooks = new[] 
            {
                new Cook(assMan, "Guybrush Threepwood", random.Next(0, 5000)),
                new Cook(assMan, "Elaine Marley", random.Next(0, 5000)),
                new Cook(assMan, "Zombie Pirate LeChuck", random.Next(0, 5000))
            };
            var multiplexer = new RoundRobinDispatcher(cooks);
            var waiter = new Waiter(multiplexer);

            list.Add(cashier);
            list.Add(assMan);
            list.AddRange(cooks.Select(x => new TaskThreadedHandler(x, x.Name)));

            Task.Run(() => MonitorStuff(list));

            for (var i = 0; i < 10; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }

            list.ForEach(item => item.Start());

            for (var i = 0; i < 10; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }

            Console.ReadLine();

            list.ForEach(item => item.Start());
        }

        private static void MonitorStuff(IEnumerable<TaskThreadedHandler> handlers)
        {
            while (true)
            {
                Thread.Sleep(2000);
                foreach (var handler in handlers)
                {
                    Console.WriteLine("{0}: {1}", handler.Name, handler.Count);
                }
            }
        }
    }
}
