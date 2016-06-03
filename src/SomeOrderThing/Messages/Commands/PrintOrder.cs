namespace SomeOrderThing.Messages.Commands
{
    public class PrintOrder : MessageBase
    {
        public PrintOrder(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
