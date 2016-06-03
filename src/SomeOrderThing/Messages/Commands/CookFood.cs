namespace SomeOrderThing.Messages.Commands
{
    public class CookFood : MessageBase
    {
        public TableOrder Order { get; set; }
    }
}
