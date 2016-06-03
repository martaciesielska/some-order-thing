namespace SomeOrderThing.Messages.Commands
{
    public class CookFood : MessageBase
    {
        public CookFood(IMessage message) : base(message)
        {
        }

        public TableOrder Order { get; set; }
    }
}
