using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors;

[ExcludeFromCodeCoverage]
public class MonthlyBudgetErrors
{
    public static Notification MONTHLY_BUDGET_NOT_FOUND = new("MONTHLY_BUDGET_NOT_FOUND", "Monthly budget not found.");

}
