namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public interface IGetMonthlyBudgetsHandler
{
    Task<GetMonthlyBudgetsResponse> HandleAsync(GetMonthlyBudgetsRequest request, CancellationToken cancellationToken);
}
