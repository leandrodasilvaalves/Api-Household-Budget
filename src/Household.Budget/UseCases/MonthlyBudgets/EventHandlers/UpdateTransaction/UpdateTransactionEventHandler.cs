using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.UpdateTransaction;

public class UpdateTransactionEventHandler : IUpdateTransactionEventHandler
{
    private readonly IMonthlyBudgetData _data;
    private readonly IDetachTransactionEventHandler _detachHandler;
    private readonly IAttachTransactionEventHandler _attachHandler;

    public UpdateTransactionEventHandler(IMonthlyBudgetData data,
                                          IDetachTransactionEventHandler detachHandler,
                                          IAttachTransactionEventHandler attachHandler)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _detachHandler = detachHandler ?? throw new ArgumentNullException(nameof(detachHandler));
        _attachHandler = attachHandler ?? throw new ArgumentNullException(nameof(attachHandler));
    }

    public async Task HandleAsync(TransactionWasUpdated notification, CancellationToken cancellationToken)
    {
        var transaction = notification?.Data ?? new();
        if (notification is not null && notification.TransactionDateHasChanged())
        {
            var detachTask = _detachHandler.HandleAsync(new TransactionWasUpdated(transaction.CloneWith(notification?.MetaData ?? [])), cancellationToken);
            var attachTask = _attachHandler.HandleAsync(new TransactionWasCreated(transaction), cancellationToken);
            await Task.WhenAll(detachTask, attachTask);
        }
        else
        {
            var year = transaction.TransactionDate.Year;
            var month = (Month)transaction.TransactionDate.Month;
            var monthlyBudget = await _data.GetOneAsync(transaction.UserId ?? "", year, month, cancellationToken);
            monthlyBudget?.UpdateTransacation(transaction);
            await _data.UpdateAsync(monthlyBudget ?? new(), cancellationToken);
        }
    }
}