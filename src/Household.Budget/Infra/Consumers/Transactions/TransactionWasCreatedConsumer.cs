using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Exceptions;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Transactions;

public class TransactionWasCreatedConsumer : IConsumer<TransactionWasCreated>
{
    private readonly IAttachTransactionEventHandler _eventHandler;

    public TransactionWasCreatedConsumer(IAttachTransactionEventHandler eventHandler)
    {
        _eventHandler = eventHandler ?? throw new ArgumentNullException(nameof(eventHandler));
    }

    public async Task Consume(ConsumeContext<TransactionWasCreated> context)
    {
        var response = await  _eventHandler.HandleAsync(context.Message, context.CancellationToken);
        if(response.IsSuccess is false)
        {
            throw new AttachTransactionExecption(response);
        }
    }
}