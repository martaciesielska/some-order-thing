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
            const string cookTopic = "cook food";
            const string priceOrderTopic = "price order";
            const string takePaymentTopic = "take payment";
            const string printOrderTopic = "print order";

            var publisher = new TopicBasedPubSub();

            var printer = new PrintingHandler();
            var cashier = new TaskThreadedHandler(new Cashier(publisher, printOrderTopic), "cashier");
            var assMan = new TaskThreadedHandler(new AssistantManager(publisher, takePaymentTopic), "assMan");

            var random = new Random();
            var cooks = new[]
            {
                new TaskThreadedHandler(new Cook(publisher, priceOrderTopic, "Guybrush Threepwood", random.Next(500, 3000)), "Guybrush Threepwood"),
                new TaskThreadedHandler(new Cook(publisher, priceOrderTopic, "Elaine Marley", random.Next(500, 3000)), "Elaine Marley"),
                new TaskThreadedHandler(new Cook(publisher, priceOrderTopic, "Zombie Pirate LeChuck", random.Next(500, 3000)), "Zombie Pirate LeChuck")
            };

            var dispatcher = new TaskThreadedHandler(new MoreFairDispatcher(cooks), "More fair handler");
            var waiter = new Waiter(publisher, cookTopic);

            var list = new List<TaskThreadedHandler>();
            list.AddRange(cooks, cashier, assMan, dispatcher);

            publisher.Subscribe(cookTopic, dispatcher);
            publisher.Subscribe(priceOrderTopic, assMan);
            publisher.Subscribe(takePaymentTopic, cashier);
            publisher.Subscribe(printOrderTopic, printer);

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

            list.ForEach(item => item.Dispose());
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
