namespace SomeOrderThing
{
    using Messages;
    using System;

    public interface IMidget : IHandle<IMessage>
    {
       event EventHandler Finished;
    }
}
