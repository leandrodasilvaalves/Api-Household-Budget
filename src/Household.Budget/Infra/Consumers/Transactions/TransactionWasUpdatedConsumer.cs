using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.UpdateTransaction;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Transactions;

public class TransactionWasUpdatedConsumer : IConsumer<TransactionWasUpdated>
{
    private readonly IUpdateTransactionEventHandler _updateHandler;
    private readonly IDetachTransactionEventHandler _detachHandler;

    public TransactionWasUpdatedConsumer(IUpdateTransactionEventHandler updateHandler,
                                         IDetachTransactionEventHandler detachHandler)
    {
        _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
        _detachHandler = detachHandler ?? throw new ArgumentNullException(nameof(detachHandler));
    }

    public Task Consume(ConsumeContext<TransactionWasUpdated> context)
    {
        var message = context.Message;
        return message.Data.Status == ModelStatus.EXCLUDED ?
            _detachHandler.HandleAsync(message, context.CancellationToken) :
            _updateHandler.HandleAsync(message, context.CancellationToken);
    }
}