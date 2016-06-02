namespace SomeOrderThing
{
    using Messages;

    public interface IPublisher
    {
        void Publish(IMessage message);
    }
}
