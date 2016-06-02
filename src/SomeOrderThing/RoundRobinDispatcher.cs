namespace SomeOrderThing
{
    using System.Linq;
    using System.Collections.Generic;
    public class RoundRobinDispatcher : IHandleOrder
    {
        private readonly Queue<IHandleOrder> handlers;

        public RoundRobinDispatcher(IEnumerable<IHandleOrder> handlers)
        {
            this.handlers = new Queue<IHandleOrder>();
            handlers.ToList().ForEach(this.handlers.Enqueue);
        }

        public void Handle(TableOrder order)
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
