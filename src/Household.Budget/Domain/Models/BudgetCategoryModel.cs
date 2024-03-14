using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

namespace Household.Budget.Domain.Models;

public class BudgetCategoryModel
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public CategoryType? Type { get; set; }
    public TotalModel Total { get; set; }
    public List<BudgetSubcategoryModel> Subcategories { get; set; }

    public void UpdatePlannedTotal(BudgetCategoryRequestViewModel subcategoryRequest) =>
        Total = (TotalModel)subcategoryRequest.PlannedTotal;

    public void CalculateTotal()
    {
        Total ??= new();
        Total.Actual = Subcategories?.Sum(x => x.Total?.Actual ?? 0) ?? 0;
    }
}
