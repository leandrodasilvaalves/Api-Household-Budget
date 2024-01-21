using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors
{
    public class BudgetError
    {
        public static Notification CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES = new("CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES", "Category planned total must be sum of subcategories");
        public static Notification BUGET_ALREADY_EXISTS => new("BUGET_ALREADY_EXISTS", "This budget already exists");
        public static Notification BUGET_NOT_FOUND => new("BUGET_NOT_FOUND", "This budget haven't found");
        public static Notification NEW_PLANNED_TOTAL_MUST_BE_GREATER_OR_EQUAL_TO_ACTUAL_TOTAL => 
            new("NEW_PLANNED_TOTAL_MUST_BE_GREATER_OR_EQUAL_TO_ACTUAL_TOTAL", "New planned total must be greater or equal to actual total");
    }
}