namespace SomeOrderThing.Messages.Events
{
    public class OrderPriced : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
