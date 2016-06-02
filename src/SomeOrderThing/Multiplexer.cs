namespace SomeOrderThing
{
    using System.Collections.Generic;

    public class Multiplexer<T> : IHandle<T>
    {
        private readonly IEnumerable<IHandle<T>> orderHandlers;

        public Multiplexer(IEnumerable<IHandle<T>> orderHandlers)
        {
            this.orderHandlers = orderHandlers;
        }
        public void Handle(T message)
        {
            foreach (var orderHandler in this.orderHandlers)
            {
                orderHandler.Handle(message);
            }
        }
    }
}
