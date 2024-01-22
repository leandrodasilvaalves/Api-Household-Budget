using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;

namespace Household.Budget.Contracts.Exceptions;

public class AttachTransactionExecption : AbstractConsumersExceptions<TransactionWasCreatedEventResponse>
{
    public AttachTransactionExecption(TransactionWasCreatedEventResponse response) : base(response) { }
}
