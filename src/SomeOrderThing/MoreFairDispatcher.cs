namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
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
            TaskThreadedHandler handler = null;
            int count = 0;

            do
            {
                for (var x = 0; x < this.handlers.Count; x++)
                {
                    handler = this.handlers.Dequeue();
                    count = handler.Count;

                    if (count < 5)
                    {
                        break;
                    }

                    this.handlers.Enqueue(handler);
                }

                Thread.Sleep(1);
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
