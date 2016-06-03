namespace SomeOrderThing.Messages.Commands
{
    public class PriceOrder : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
