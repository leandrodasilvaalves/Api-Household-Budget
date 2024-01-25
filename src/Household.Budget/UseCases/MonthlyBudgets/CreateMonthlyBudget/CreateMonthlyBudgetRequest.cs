using Household.Budget.Contracts.Enums;

namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public class CreateMonthlyBudgetRequest : Request
{
    public int Year { get; set; }
    public Month Month { get; set; }
    public List<BudgetCategoryRequestViewModel> Categories { get; set; } = [];

    public override void Validate() =>
        AddNotifications(new CreateMonthlyBudgetRequestContract(this));
}

public class BudgetCategoryRequestViewModel
{
    public string Id { get; set; } = "";
    public decimal PlannedTotal { get; set; }

    public List<BudgetCategoryRequestViewModel> Subcategories { get; set; } = [];
}