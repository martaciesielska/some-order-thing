namespace SomeOrderThing.Messages.Events
{
    public class OrderPriced : MessageBase, IEvent
    {
        public OrderPriced(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
