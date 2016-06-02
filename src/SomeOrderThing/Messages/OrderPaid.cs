namespace SomeOrderThing.Messages
{
    public class OrderPaid : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
