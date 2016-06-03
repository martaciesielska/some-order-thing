namespace SomeOrderThing.Messages.Commands
{
    public class TakePayment : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
