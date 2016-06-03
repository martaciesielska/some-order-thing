namespace SomeOrderThing
{
    using Messages.Events;
    using Messages.Commands;

    public class Cashier : IHandle<TakePayment>
    {
        private readonly IPublisher publisher;

        public Cashier(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(TakePayment command)
        {
            var tableOrder = command.Order.Copy();
            tableOrder.Paid = true;
            this.publisher.Publish(new OrderPaid() { Order = tableOrder });
        }
    }
}
