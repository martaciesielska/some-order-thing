namespace SomeOrderThing
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var order = new TableOrder(Guid.NewGuid());

            var printer = new PrintingHandler();
            var assMan = new AssistantManager(printer);
            var cook = new Cook(assMan);
            var waiter = new Waiter(cook);
            
            waiter.Handle(order);
        }
    }
}
