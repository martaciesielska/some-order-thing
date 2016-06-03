namespace SomeOrderThing.Messages.Commands
{
    public class TakePayment : MessageBase
    {
        public TakePayment(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
