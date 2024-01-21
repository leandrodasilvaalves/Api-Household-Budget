using Household.Budget.Contracts.Data;

namespace Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

public class UpdateMonthlyBudgetHandler : IUpdateMonthlyBudgetHandler
{
    private readonly IMonthlyBudgeRepository _monthlyBudgeRepository;

    public UpdateMonthlyBudgetHandler(IMonthlyBudgeRepository monthlyBudgeRepository)
    {
        _monthlyBudgeRepository = monthlyBudgeRepository ?? throw new ArgumentNullException(nameof(monthlyBudgeRepository));
    }

    public async Task<UpdateMonthlyBudgetResponse> HandleAsync(UpdateMonthlyBudgetRequest request,
                                                    CancellationToken cancellationToken)
    {

        var monthlyBudgetTask = _monthlyBudgeRepository.GetByIdAsync(request.Id, request.UserId, cancellationToken);
        var alreadyExistsTask = _monthlyBudgeRepository.GetOneAsync(
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
        await _monthlyBudgeRepository.UpdateAsync(monthlyBudget, cancellationToken);
        return new UpdateMonthlyBudgetResponse(monthlyBudget);
    }
}
