namespace SomeOrderThing.Messages
{
    using System;

    public class MessageBase : IMessage
    {
        public Guid MessageId { get; set; }

        public Guid CorrelationId { get; set; }

        public Guid CausationId { get; set; }

        public MessageBase()
        {
            this.MessageId = Guid.NewGuid();
        }

        public MessageBase(IMessage message)
        {
            this.MessageId = Guid.NewGuid();
            this.CorrelationId = message.CorrelationId;
            this.CausationId = message.MessageId;
        }
    }
}
