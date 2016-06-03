namespace SomeOrderThing.Messages.Commands
{
    public class PriceOrder : MessageBase
    {
        public PriceOrder(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
