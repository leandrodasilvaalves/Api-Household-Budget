using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;

public class DetachTransactionEventHandler : IDetachTransactionEventHandler
{
    private readonly IMonthlyBudgetRepository _repository;

    public DetachTransactionEventHandler(IMonthlyBudgetRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task HandleAsync(TransactionWasUpdated notification, CancellationToken cancellationToken)
    {
        var transaction = notification?.Data;
        var transactionDate = transaction?.TransactionDate;
        var monthlyBudget = await _repository.GetOneAsync(transaction?.UserId, transactionDate.Value.Year, (Month)transactionDate.Value.Month, cancellationToken);

        monthlyBudget?.DetachTransaction(transaction);
        await _repository.UpdateAsync(monthlyBudget, cancellationToken);
    }
}
