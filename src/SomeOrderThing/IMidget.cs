namespace SomeOrderThing
{
    using Messages.Events;
    using System;

    public interface IMidget : IHandle<IEvent>
    {
       event EventHandler Finished;
    }
}
