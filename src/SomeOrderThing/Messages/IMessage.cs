namespace SomeOrderThing.Messages
{
    using System;

    public interface IMessage
    {
        Guid Id { get; }
    }
}
