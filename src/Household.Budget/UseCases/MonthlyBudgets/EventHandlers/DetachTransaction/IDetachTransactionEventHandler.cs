using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;

public interface IDetachTransactionEventHandler
{
    Task HandleAsync(TransactionWasUpdated notification, CancellationToken cancellationToken);
}
