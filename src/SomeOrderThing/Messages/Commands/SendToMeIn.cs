namespace SomeOrderThing.Messages.Commands
{
    public class SendToMeIn : MessageBase
    {
        public SendToMeIn(IMessage message) : base(message)
        {
        }

        public IMessage Message { get; set; }

        public int Seconds { get; set; }
    }
}
