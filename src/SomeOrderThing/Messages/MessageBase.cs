namespace SomeOrderThing.Messages
{
    using System;

    public class MessageBase : IMessage
    {
        public Guid Id { get; set; }

        public MessageBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
