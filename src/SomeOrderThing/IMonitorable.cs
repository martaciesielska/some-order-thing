namespace SomeOrderThing
{
    public interface IMonitorable : IStartable
    {
        string Name { get; }

        int Count { get; }
    }
}
