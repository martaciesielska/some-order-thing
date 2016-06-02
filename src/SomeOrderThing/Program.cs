namespace SomeOrderThing
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new PrintingHandler();
            var cashier = new Cashier(printer);
            var assMan = new AssistantManager(cashier);
            var cooks = new Cook[] { new Cook(assMan), new Cook(assMan), new Cook(assMan) };
            var multiplexer = new Multiplexer(cooks);
            var waiter = new Waiter(multiplexer);

            for (var i = 0; i < 10; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }
        }
    }
}
