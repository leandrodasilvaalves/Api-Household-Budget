namespace Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

public interface IUpdateMonthlyBudgetHandler
{
    Task<UpdateMonthlyBudgetResponse> HandleAsync(UpdateMonthlyBudgetRequest request, CancellationToken cancellationToken);
}
