using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public class GetMonthlyBudgetsHandler : IGetMonthlyBudgetsHandler
{
    private readonly IMonthlyBudgetData _Data;

    public GetMonthlyBudgetsHandler(IMonthlyBudgetData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<GetMonthlyBudgetsResponse> HandleAsync(GetMonthlyBudgetsRequest request, CancellationToken cancellationToken)
    {
        var result = await _Data.GetOneAsync(request.UserId, request.Year, request.Month, cancellationToken);
        return new GetMonthlyBudgetsResponse(result);
    }
}