namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<TaskThreadedHandler>();
            var random = new Random();

            var publisher = new TopicBasedPubSub();
            var printer = new PrintingHandler();
            var cashier = new TaskThreadedHandler(new Cashier(publisher), "cashier");
            var assMan = new TaskThreadedHandler(new AssistantManager(publisher), "assMan");

            var cooks = new[]
            {
                new TaskThreadedHandler(new Cook(publisher, "Guybrush Threepwood", random.Next(500, 3000)), "Guybrush Threepwood"),
                new TaskThreadedHandler(new Cook(publisher, "Elaine Marley", random.Next(500, 3000)), "Elaine Marley"),
                new TaskThreadedHandler(new Cook(publisher, "Zombie Pirate LeChuck", random.Next(500, 3000)), "Zombie Pirate LeChuck")
            };

            var dispatcher = new TaskThreadedHandler(new MoreFairDispatcher(cooks), "More fair handler");
            var waiter = new Waiter(publisher);

            list.Add(cashier);
            list.Add(assMan);
            list.Add(dispatcher);
            list.AddRange(cooks);


            publisher.Subscribe("cook food", dispatcher);
            publisher.Subscribe("price order", assMan);
            publisher.Subscribe("take payment", cashier);
            publisher.Subscribe("print order", printer);

            var cts = new CancellationTokenSource();
            Task.Run(() => MonitorStuff(list, cts.Token));

            list.ForEach(item => item.Start());

            for (var i = 0; i < 50; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }

            Console.ReadLine();

            cts.Cancel();

            list.ForEach(item => item.Start());
        }

        private static void MonitorStuff(IEnumerable<TaskThreadedHandler> handlers, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var handler in handlers)
                {
                    Console.Write("{0}: {1}, ", handler.Name, handler.Count);
                }

                Console.WriteLine();

                Thread.Sleep(1000);
            }
        }
    }
}
