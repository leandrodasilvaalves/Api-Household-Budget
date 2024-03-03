using Household.Budget.Domain.Models;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment;

using MassTransit;

namespace Household.Budget.Infra.Consumers.MonthlyBudget
{
    public class AttachTransactionNextPaymentConsumer : IConsumer<BudgetTransactionWithCategoryModel>
    {
        private readonly IAttachTransactionNextPaymentEventHandler _handler;

        public AttachTransactionNextPaymentConsumer(IAttachTransactionNextPaymentEventHandler handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public Task Consume(ConsumeContext<BudgetTransactionWithCategoryModel> context)
        {
            Console.WriteLine("message: {0}", System.Text.Json.JsonSerializer.Serialize(context.Message,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));

            return _handler.HandleAsync(context.Message, context.CancellationToken);
        }
    }
}