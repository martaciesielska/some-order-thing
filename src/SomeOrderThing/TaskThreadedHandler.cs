namespace SomeOrderThing
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskThreadedHandler : IHandleOrder, IStartable, IDisposable
    {
        private readonly ConcurrentQueue<TableOrder> orders = new ConcurrentQueue<TableOrder>();
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        private readonly IHandleOrder handler;

        public TaskThreadedHandler(IHandleOrder handler, string name)
        {
            this.handler = handler;
            this.Name = name;
        }

        public string Name { get; private set; }

        public int Count
        {
            get { return this.orders.Count; }
        }

        public void Handle(TableOrder order)
        {
            this.orders.Enqueue(order);
        }

        public void Start()
        {
            Task.Factory.StartNew(() => this.DoStuff(this.tokenSource.Token), TaskCreationOptions.LongRunning);
        }

        private void DoStuff(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
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
            this.tokenSource.Cancel();
        }
    }
}
