namespace SomeOrderThing
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    public class ThreadedHandler : IHandleOrder, IStartable, IDisposable
    {
        private readonly ConcurrentQueue<TableOrder> orders;
        private readonly IHandleOrder handler;
        private Thread thread;

        public ThreadedHandler(IHandleOrder handler)
        {
            this.handler = handler;
            this.orders = new ConcurrentQueue<TableOrder>();
        }

        public void Handle(TableOrder order)
        {
            this.orders.Enqueue(order);
        }

        public void Start()
        {
            this.thread = new Thread(new ThreadStart(DoStuff));
            thread.Start();
        }

        private void DoStuff()
        {
            while (true)
            {
                TableOrder order;
                if (this.orders.TryDequeue(out order))
                {
                    try
                    {
                        this.handler.Handle(order);
                    }
                    catch (Exception)
                    {
                    }
                }

                Thread.Sleep(1);
            }
        }

        public void Dispose()
        {
            this.thread.Abort();
        }
    }
}
