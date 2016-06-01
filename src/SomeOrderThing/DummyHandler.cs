namespace SomeOrderThing
{
    using System.Threading;
    public class DummyHandler : IHandleOrder
    {
        public void Handle(TableOrder order)
        {
            Thread.Sleep(100);
        }
    }
}
