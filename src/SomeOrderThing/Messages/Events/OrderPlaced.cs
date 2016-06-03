namespace SomeOrderThing.Messages.Events
{
    public class OrderPlaced : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
