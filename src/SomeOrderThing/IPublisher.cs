namespace SomeOrderThing
{
    using Messages;

    public interface IPublisher
    {
        void Publish<T>(T message) where T : IMessage;
    }
}
