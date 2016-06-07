namespace SomeOrderThing
{
    using System;
    using Messages;
    using Messages.Commands;
    using Messages.Events;

    public class ZimbabwianMidget : IHandle<IMessage>, IMidget
    {
        private readonly IPublisher publisher;
        public event EventHandler Finished;
        private bool isCooked;

        public ZimbabwianMidget(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Handle(IMessage message)
        {
            if (message is IEvent)
            {
                this.HandleInternal((dynamic)message);
            }
        }

        private void HandleInternal(OrderPaid message)
        {
            this.publisher.Publish(
                new SendToMeIn(message)
                {
                    Message = new RetryCooking(message),
                    Seconds = 5
                });

            this.publisher.Publish(new CookFood(message) { Order = message.Order });
        }

        private void HandleInternal(OrderPriced message)
        {
            this.publisher.Publish(new TakePayment(message) { Order = message.Order });
        }

        private void HandleInternal(OrderCooked message)
        {
            this.isCooked = true;

            this.Finished.Invoke(this, new MidgetEventArgs() { CorrelationId = message.CorrelationId });
            this.publisher.Publish(new PrintOrder(message) { Order = message.Order });
        }

        private void HandleInternal(OrderPlaced message)
        {
            this.publisher.Publish(new PriceOrder(message) { Order = message.Order });
        }

        private void HandleInternal(RetryCooking message)
        {
            if (this.isCooked)
            {
                return;
            }

            this.publisher.Publish(
                new SendToMeIn(message)
                {
                    Message = new RetryCooking(message),
                    Seconds = 5
                });

            this.publisher.Publish(new CookFood(message) { Order = message.Order });
        }
    }
}
