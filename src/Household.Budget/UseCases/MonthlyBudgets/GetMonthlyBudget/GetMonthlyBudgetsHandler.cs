using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public class GetMonthlyBudgetsHandler : IGetMonthlyBudgetsHandler
{
    private readonly IMonthlyBudgetData _data;

    public GetMonthlyBudgetsHandler(IMonthlyBudgetData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<GetMonthlyBudgetsResponse> HandleAsync(GetMonthlyBudgetsRequest request, CancellationToken cancellationToken)
    {
        var result = await _data.GetOneAsync(request.UserId, request.Year, request.Month, cancellationToken);
        return result == null
            ? new GetMonthlyBudgetsResponse(MonthlyBudgetErrors.MONTHLY_BUDGET_NOT_FOUND)
            : new GetMonthlyBudgetsResponse(result);
    }
}