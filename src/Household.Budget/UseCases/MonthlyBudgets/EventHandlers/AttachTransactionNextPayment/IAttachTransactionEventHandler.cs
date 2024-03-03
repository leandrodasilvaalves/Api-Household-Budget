using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment
{
    public interface IAttachTransactionNextPaymentEventHandler
    {
        Task HandleAsync(BudgetTransactionWithCategoryModel request, CancellationToken cancellationToken);
    }
}