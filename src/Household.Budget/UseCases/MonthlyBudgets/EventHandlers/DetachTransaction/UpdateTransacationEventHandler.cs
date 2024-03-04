using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;

public class DetachTransactionEventHandler : IDetachTransactionEventHandler
{
    private readonly IMonthlyBudgetData _Data;

    public DetachTransactionEventHandler(IMonthlyBudgetData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task HandleAsync(TransactionWasUpdated notification, CancellationToken cancellationToken)
    {
        var transaction = notification?.Data;
        var transactionDate = transaction?.TransactionDate;
        var monthlyBudget = await _Data.GetOneAsync(transaction?.UserId, transactionDate.Value.Year, (Month)transactionDate.Value.Month, cancellationToken);

        monthlyBudget?.DetachTransaction(transaction);
        await _Data.UpdateAsync(monthlyBudget, cancellationToken);
    }
}
