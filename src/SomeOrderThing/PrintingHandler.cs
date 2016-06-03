namespace SomeOrderThing
{
    using System;
    using Messages.Commands;

    public class PrintingHandler : IHandle<PrintOrder>
    {
        public void Handle(PrintOrder command)
        {
            ////Console.WriteLine(command.Order.Serialize().ToString());
        }
    }
}
