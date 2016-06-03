namespace SomeOrderThing.Messages.Events
{
    public class OrderCooked : MessageBase, IEvent
    {
        public OrderCooked(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
