using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

public class UpdateMonthlyBudgetHandler : IUpdateMonthlyBudgetHandler
{
    private readonly IMonthlyBudgetData _monthlyBudgetData;

    public UpdateMonthlyBudgetHandler(IMonthlyBudgetData monthlyBudgetData)
    {
        _monthlyBudgetData = monthlyBudgetData ?? throw new ArgumentNullException(nameof(monthlyBudgetData));
    }

    public async Task<UpdateMonthlyBudgetResponse> HandleAsync(UpdateMonthlyBudgetRequest request,
                                                    CancellationToken cancellationToken)
    {

        var monthlyBudgetTask = _monthlyBudgetData.GetByIdAsync(request.Id, request.UserId, cancellationToken);
        var alreadyExistsTask = _monthlyBudgetData.GetOneAsync(
            x => x.Id != request.Id && x.UserId == request.UserId && x.Year == request.Year && x.Month == request.Month, cancellationToken);

        await Task.WhenAll(monthlyBudgetTask, alreadyExistsTask);
        var monthlyBudget = monthlyBudgetTask.Result;
        var alreadyExists = alreadyExistsTask.Result;

        var contract = new UpdateMonthlyBudgetRequestContract(monthlyBudget, alreadyExists is { }, request);
        if (contract.IsValid is false)
        {
            return new UpdateMonthlyBudgetResponse(contract.Notifications);
        }

        monthlyBudget?.Merge(request);
        await _monthlyBudgetData.UpdateAsync(monthlyBudget, cancellationToken);
        return new UpdateMonthlyBudgetResponse(monthlyBudget);
    }
}
