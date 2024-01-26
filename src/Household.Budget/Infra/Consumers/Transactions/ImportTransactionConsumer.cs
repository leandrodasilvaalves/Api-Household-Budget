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

        public Task Consume(ConsumeContext<CreateTransactionRequest> context) =>
            _handler.HandleAsync(context.Message, context.CancellationToken);
    }
}