namespace SomeOrderThing
{
    public interface IPublisher
    {
        void Publish(string topic, TableOrder order);
    }
}
