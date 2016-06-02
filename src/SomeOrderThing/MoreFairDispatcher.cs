namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;

    public class MoreFairDispatcher : IHandleOrder
    {
        private readonly Queue<TaskThreadedHandler> handlers;

        public MoreFairDispatcher(IEnumerable<TaskThreadedHandler> handlers)
        {
            this.handlers = new Queue<TaskThreadedHandler>(handlers);

            if (this.handlers.Count <= 0)
            {
                throw new ArgumentException("No handlers!");
            }
        }

        public void Handle(TableOrder order)
        {
            TaskThreadedHandler handler;
            int count = 0;

            do
            {
                handler = this.handlers.Dequeue();
                count = handler.Count;

                if (count >= 5)
                {
                    this.handlers.Enqueue(handler);
                }
            }
            while (count >= 5);

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
