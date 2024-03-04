using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public class CreateMonthlyBudgetHandler : ICreateMonthlyBudgetHandler
{
    private readonly IMonthlyBudgetData _monthlyBudgeData;
    private readonly ICategoryData _categoryData;

    public CreateMonthlyBudgetHandler(IMonthlyBudgetData monthlyBudgeData,
                                      ICategoryData categoryData)
    {
        _monthlyBudgeData = monthlyBudgeData ?? throw new ArgumentNullException(nameof(monthlyBudgeData));
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
    }

    public async Task<CreateMonthlyBudgetResponse> HandleAsync(CreateMonthlyBudgetRequest request,
                                                               CancellationToken cancellationToken)
    {

        if (await _monthlyBudgeData.ExistsAsync(
            request.UserId, request.Year, request.Month, cancellationToken))
        {
            return new CreateMonthlyBudgetResponse(BudgetError.BUGET_ALREADY_EXISTS);
        }

        var categories = await GetAllCategoriesAsync(request.UserId, cancellationToken);
        var contract = new CreateMonthlyBudgetRequestContract(request, categories);
        if (contract.IsValid is false)
        {
            return new CreateMonthlyBudgetResponse(contract.Notifications);
        }

        var monthlyBudget = new MonthlyBudget();
        monthlyBudget.Create(request, categories);
        await _monthlyBudgeData.CreateAsync(monthlyBudget, cancellationToken);
        return new CreateMonthlyBudgetResponse(monthlyBudget);
    }

    private async Task<List<Category>> GetAllCategoriesAsync(string userId, CancellationToken cancellationToken)
    {
        bool hasMorePage;
        var (pageNumber, pageSize) = (1, 50);
        List<Category> categories = [];
        do
        {
            var result = await _categoryData.GetAllAsync(pageSize, pageNumber, userId, cancellationToken);
            hasMorePage = result.HasMorePages;
            pageNumber++;
            categories.AddRange(result.Items ?? []);
        } while (hasMorePage);
        return categories;
    }
}
