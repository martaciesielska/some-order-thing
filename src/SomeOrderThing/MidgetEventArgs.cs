namespace SomeOrderThing
{
    using System;

    public class MidgetEventArgs : EventArgs
    {
        public Guid CorrelationId { get; set; }
    }
}
