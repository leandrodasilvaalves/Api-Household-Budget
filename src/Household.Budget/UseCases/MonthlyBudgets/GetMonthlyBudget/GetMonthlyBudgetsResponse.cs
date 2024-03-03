using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public class GetMonthlyBudgetsResponse : AbstractResponse<MonthlyBudget>
{
    public  GetMonthlyBudgetsResponse(MonthlyBudget data) : base(data) { }
    public GetMonthlyBudgetsResponse(IEnumerable<Notification> errors) : base(errors) { }
    public GetMonthlyBudgetsResponse(Notification notification) : base(notification) { }
}