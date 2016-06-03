namespace SomeOrderThing.Messages.Events
{
    public class OrderCooked : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
