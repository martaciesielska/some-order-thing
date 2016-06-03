namespace SomeOrderThing
{
    using System;

    public interface ISubscriber
    {
        void SubscribeByType<T>(IHandle<T> handler);

        void SubscribeByCorrelationId<T>(Guid correlationId, IHandle<T> handler);

        void Subscribe<T>(string topic, IHandle<T> handler);

        void UnsubscribeByType<T>(IHandle<T> handler);

        void UnsubscribeByCorrelationId<T>(Guid correlationId, IHandle<T> handler);

        void Unsubscribe<T>(string topic, IHandle<T> handler);
    }
}
