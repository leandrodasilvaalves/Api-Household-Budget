
using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public class CreateMonthlyBudgetResponse : AbstractResponse<MonthlyBudget>
{
    public CreateMonthlyBudgetResponse(MonthlyBudget data) : base(data) { }
    public CreateMonthlyBudgetResponse(IEnumerable<Notification> errors) : base(errors) { }
    public CreateMonthlyBudgetResponse(Notification notification) : base(notification) { }
}