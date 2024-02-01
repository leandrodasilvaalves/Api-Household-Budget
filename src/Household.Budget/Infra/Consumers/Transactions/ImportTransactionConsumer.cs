using Household.Budget.Contracts.Exceptions;
using Household.Budget.UseCases.Transactions.CreateTransaction;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Transactions
{
    public class ImportTransactionConsumer : IConsumer<CreateTransactionRequest>
    {
        private readonly ICreateTransactionHandler _handler;

        public ImportTransactionConsumer(ICreateTransactionHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<CreateTransactionRequest> context)
        {
            var response = await _handler.HandleAsync(context.Message, context.CancellationToken);
            if (response.IsSuccess is false)
            {
                throw new ImportTransactionException(response);
            }
        }
    }
}