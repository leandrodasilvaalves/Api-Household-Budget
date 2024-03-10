using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;

public class DetachTransactionEventHandler : IDetachTransactionEventHandler
{
    private readonly IMonthlyBudgetData _data;

    public DetachTransactionEventHandler(IMonthlyBudgetData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task HandleAsync(TransactionWasUpdated notification, CancellationToken cancellationToken)
    {
        var transaction = notification?.Data;
        var transactionDate = transaction?.TransactionDate;
        var monthlyBudget = await _data.GetOneAsync(transaction?.UserId, transactionDate.Value.Year,
                                                    (Month)transactionDate.Value.Month, cancellationToken);

        if (monthlyBudget is { })
        {
            monthlyBudget.DetachTransaction(transaction);
            await _data.UpdateAsync(monthlyBudget, cancellationToken);
        }
    }
}
