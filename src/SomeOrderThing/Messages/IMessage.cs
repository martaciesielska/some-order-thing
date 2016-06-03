namespace SomeOrderThing.Messages
{
    using System;

    public interface IMessage
    {
        Guid MessageId { get; }

        Guid CorrelationId { get; }

        Guid CausationId { get; }
    }
}
