using Household.Budget.Contracts.Data;

namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public class GetMonthlyBudgetsHandler : IGetMonthlyBudgetsHandler
{
    private readonly IMonthlyBudgeRepository _repository;

    public GetMonthlyBudgetsHandler(IMonthlyBudgeRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetMonthlyBudgetsResponse> HandleAsync(GetMonthlyBudgetsRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetOneAsync(request.UserId, request.Year, request.Month, cancellationToken);
        return new GetMonthlyBudgetsResponse(result);
    }
}