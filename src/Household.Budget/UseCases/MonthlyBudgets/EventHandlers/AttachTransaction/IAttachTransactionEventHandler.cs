using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction
{
    public interface IAttachTransactionEventHandler
    {
        Task<TransactionWasCreatedEventResponse> HandleAsync(TransactionWasCreated notification, CancellationToken cancellationToken);
    }
}