namespace SomeOrderThing
{
    using System;
    using Messages.Commands;
    using Messages;
    public class Monitor : IHandle<IMessage>
    {
        public void Handle(IMessage message)
        {
            Console.WriteLine(
                "Type: {3}, MsgId: {0}, CorrId: {1}, CauseId: {2}",
                message.MessageId,
                message.CorrelationId,
                message.CausationId,
                message.GetType().Name);
        }
    }
}
