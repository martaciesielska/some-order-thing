namespace SomeOrderThing.Messages.Commands
{
    using SomeOrderThing.Messages.Events;

    public class RetryCooking : MessageBase, IEvent
    {
        public RetryCooking(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
