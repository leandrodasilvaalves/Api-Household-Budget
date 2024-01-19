using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors
{
    public class BudgetError
    {
        public static Notification CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES = new("CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES", "Category planned total must be sum of subcategories");
    }
}