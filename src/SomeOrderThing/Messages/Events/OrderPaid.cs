namespace SomeOrderThing.Messages.Events
{
    public class OrderPaid : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
