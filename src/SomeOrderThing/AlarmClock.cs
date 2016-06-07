
namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using Messages;
    using Messages.Commands;
    using System.Threading;

    public class AlarmClock : IHandle<SendToMeIn>
    {
        private readonly IPublisher publisher;
        private Timer timer;
        private IDictionary<DateTime, IMessage> messages = new Dictionary<DateTime, IMessage>();

        public AlarmClock(IPublisher publisher)
        {
            this.publisher = publisher;
            this.timer = new Timer(new TimerCallback(Tick));
            this.timer.Change(1000, Timeout.Infinite);
        }

        public void Handle(SendToMeIn message)
        {
            var retryMessage = message.Message;
            var now = DateTime.Now;
            var when = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddSeconds(message.Seconds);

            this.messages.Add(when, retryMessage);
        }

        private void Tick(object state)
        {
            this.timer.Change(1000, Timeout.Infinite);

            var now = DateTime.Now;
            var when = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

            if (this.messages.ContainsKey(when))
            {
                this.publisher.Publish(this.messages[when]);
            }
        }
    }
}
