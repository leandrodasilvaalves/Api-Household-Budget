namespace Household.Budget.Contracts.Events;

public abstract class Event<TData> : IEvent<TData>
{
    protected Event() => SendedAt = DateTime.UtcNow;

    public abstract string Name { get; }

    public abstract TData Data { get; }

    public DateTime SendedAt { get; }
}
