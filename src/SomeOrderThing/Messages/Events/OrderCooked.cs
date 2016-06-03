namespace SomeOrderThing.Messages.Events
{
    public class OrderCooked : MessageBase
    {
        public OrderCooked(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
