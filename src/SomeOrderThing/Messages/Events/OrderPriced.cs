namespace SomeOrderThing.Messages.Events
{
    public class OrderPriced : MessageBase
    {
        public OrderPriced(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
