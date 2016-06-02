namespace SomeOrderThing.Messages
{
    public class OrderPlaced : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
