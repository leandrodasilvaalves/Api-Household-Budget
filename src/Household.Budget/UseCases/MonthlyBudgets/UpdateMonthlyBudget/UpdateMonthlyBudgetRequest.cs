using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

namespace Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

public class UpdateMonthlyBudgetRequest : Request
{
    public string Id { get; set; } = "";
    public int? Year { get; set; }
    public Month? Month { get; set; }
    public ModelStatus? Status { get; set; }

    public List<BudgetCategoryRequestViewModel> Categories { get; set; } = [];

    public override void Validate() =>
        AddNotifications(new UpdateMonthlyBudgetRequestContract(this));
}