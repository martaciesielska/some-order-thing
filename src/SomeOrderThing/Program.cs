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
                new TaskThreadedHandler(new Cook(assMan, "Guybrush Threepwood", random.Next(500, 3000)), "Guybrush Threepwood"),
                new TaskThreadedHandler(new Cook(assMan, "Elaine Marley", random.Next(500, 3000)), "Elaine Marley"),
                new TaskThreadedHandler(new Cook(assMan, "Zombie Pirate LeChuck", random.Next(500, 3000)), "Zombie Pirate LeChuck")
            };

            var dispatcher = new RoundRobinDispatcher(cooks);
            var waiter = new Waiter(dispatcher);

            list.Add(cashier);
            list.Add(assMan);
            list.AddRange(cooks);

            Task.Run(() => MonitorStuff(list));

            list.ForEach(item => item.Start());

            for (var i = 0; i < 20; i++)
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
                Thread.Sleep(500);
                foreach (var handler in handlers)
                {
                    Console.WriteLine("{0}: {1}", handler.Name, handler.Count);
                }
            }
        }
    }
}
