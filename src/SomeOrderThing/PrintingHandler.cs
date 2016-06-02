namespace SomeOrderThing
{
    using System;
    using Messages;

    public class PrintingHandler : IHandle<OrderPaid>
    {
        public void Handle(OrderPaid orderPaid)
        {
            ////Console.WriteLine(orderPaid.Order.Serialize().ToString());
        }
    }
}
