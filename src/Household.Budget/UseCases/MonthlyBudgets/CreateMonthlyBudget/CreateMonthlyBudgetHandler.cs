using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public class CreateMonthlyBudgetHandler : ICreateMonthlyBudgetHandler
{
    private readonly IMonthlyBudgeRepository _monthlyBudgeRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateMonthlyBudgetHandler(IMonthlyBudgeRepository monthlyBudgeRepository,
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

        var categoriesIds = request.Categories.Select(x => x.Id).ToArray();
        bool hasMorePage = false;
        List<Category> categories = [];
        do
        {
            var (pageNumber, pageSize) = (1, 50);
            var result = await _categoryRepository.GetAllAsync(
                pageSize, pageNumber, request.UserId, cancellationToken, categoriesIds);

            hasMorePage = result.HasMorePages;
            pageNumber++;
            categories.AddRange(result.Items ?? []);
        } while (hasMorePage);

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
}
