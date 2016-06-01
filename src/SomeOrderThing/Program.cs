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
            var cook = new Cook(assMan);
            var waiter = new Waiter(cook);

            for (var i = 0; i < 10; i++)
            {
                var order = new TableOrder(Guid.NewGuid());
                waiter.Handle(order);
            }
        }
    }
}
