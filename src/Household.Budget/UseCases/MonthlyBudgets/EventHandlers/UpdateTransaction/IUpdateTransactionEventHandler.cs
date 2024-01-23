using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.UpdateTransaction;

public interface IUpdateTransactionEventHandler
{
    Task HandleAsync(TransactionWasUpdated notification, CancellationToken cancellationToken);
}
