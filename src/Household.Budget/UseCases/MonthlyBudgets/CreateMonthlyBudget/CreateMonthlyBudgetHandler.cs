using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public class CreateMonthlyBudgetHandler : ICreateMonthlyBudgetHandler
{
    private readonly IMonthlyBudgetRepository _monthlyBudgeRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateMonthlyBudgetHandler(IMonthlyBudgetRepository monthlyBudgeRepository,
                                      ICategoryRepository categoryRepository)
    {
        _monthlyBudgeRepository = monthlyBudgeRepository ?? throw new ArgumentNullException(nameof(monthlyBudgeRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<CreateMonthlyBudgetResponse> HandleAsync(CreateMonthlyBudgetRequest request,
                                                               CancellationToken cancellationToken)
    {

        if (await _monthlyBudgeRepository.ExistsAsync(
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
        await _monthlyBudgeRepository.CreateAsync(monthlyBudget, cancellationToken);
        return new CreateMonthlyBudgetResponse(monthlyBudget);
    }

    private async Task<List<Category>> GetAllCategoriesAsync(string userId, CancellationToken cancellationToken)
    {
        bool hasMorePage;
        var (pageNumber, pageSize) = (1, 50);
        List<Category> categories = [];
        do
        {
            var result = await _categoryRepository.GetAllAsync(pageSize, pageNumber, userId, cancellationToken);
            hasMorePage = result.HasMorePages;
            pageNumber++;
            categories.AddRange(result.Items ?? []);
        } while (hasMorePage);
        return categories;
    }
}
