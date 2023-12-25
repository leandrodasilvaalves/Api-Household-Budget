using MediatR;

namespace Household.Budget.Contracts.Events;

public interface IEvent<TData> : INotification
{
    public string Name { get; }
    public TData Data { get; }
    public DateTime SendedAt { get; }
}