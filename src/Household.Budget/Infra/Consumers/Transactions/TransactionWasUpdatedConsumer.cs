using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.UpdateTransaction;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Transactions;

public class TransactionWasUpdatedConsumer : IConsumer<TransactionWasUpdated>
{
    private readonly IUpdateTransactionEventHandler _handler;

    public TransactionWasUpdatedConsumer(IUpdateTransactionEventHandler handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public Task Consume(ConsumeContext<TransactionWasUpdated> context) => 
        _handler.HandleAsync(context.Message, context.CancellationToken);
}