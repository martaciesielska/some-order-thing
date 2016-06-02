namespace SomeOrderThing.Messages
{
    public class OrderPriced : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
