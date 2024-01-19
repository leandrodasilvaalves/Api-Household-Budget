namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public interface ICreateMonthlyBudgetHandler
{
    Task<CreateMonthlyBudgetResponse> HandleAsync(CreateMonthlyBudgetRequest request, CancellationToken cancellationToken);
}
