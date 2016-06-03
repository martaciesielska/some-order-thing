using System;

namespace SomeOrderThing
{
    public class DroppingHandler<T> : IHandle<T>
    {
        private readonly IHandle<T> handler;
        private static readonly Random Random = new Random();

        public DroppingHandler(IHandle<T> handler)
        {
            this.handler = handler;
        }

        public void Handle(T message)
        {
            if (Random.Next(10) == 1)
            {
                return;
            }

            this.handler.Handle(message);
        }
    }
}
