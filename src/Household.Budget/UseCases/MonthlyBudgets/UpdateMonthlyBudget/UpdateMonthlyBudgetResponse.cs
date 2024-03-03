using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

public class UpdateMonthlyBudgetResponse : AbstractResponse<MonthlyBudget>
{
    public UpdateMonthlyBudgetResponse(MonthlyBudget data) : base(data) { }
    public UpdateMonthlyBudgetResponse(IEnumerable<Notification> errors) : base(errors){}
    public UpdateMonthlyBudgetResponse(Notification notification) : base(notification){}    
}
