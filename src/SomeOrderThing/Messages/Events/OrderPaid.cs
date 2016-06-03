namespace SomeOrderThing.Messages.Events
{
    public class OrderPaid : MessageBase, IEvent
    {
        public OrderPaid(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
