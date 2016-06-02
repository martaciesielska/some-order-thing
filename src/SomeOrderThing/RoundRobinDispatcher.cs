namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;

    public class RoundRobinDispatcher<T> : IHandle<T>
    {
        private readonly Queue<IHandle<T>> handlers;

        public RoundRobinDispatcher(IEnumerable<IHandle<T>> handlers)
        {
            this.handlers = new Queue<IHandle<T>>(handlers);

            if (this.handlers.Count <= 0)
            {
                throw new ArgumentException("No handlers!");
            }
        }

        public void Handle(T order)
        {
            var handler = this.handlers.Dequeue();

            try
            {
                handler.Handle(order);
            }
            finally
            {
                this.handlers.Enqueue(handler);
            }
        }
    }
}
