namespace SomeOrderThing
{
    using System;
    using System.Threading;
    public class DummyHandler : IHandleOrder
    {
        public void Handle(TableOrder order)
        {
            Console.WriteLine(order.Serialize().ToString());
        }
    }
}
