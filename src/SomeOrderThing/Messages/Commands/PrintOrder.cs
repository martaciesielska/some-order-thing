namespace SomeOrderThing.Messages.Commands
{
    public class PrintOrder : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
