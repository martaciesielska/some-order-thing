namespace SomeOrderThing.Messages
{
    public class OrderCooked : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
