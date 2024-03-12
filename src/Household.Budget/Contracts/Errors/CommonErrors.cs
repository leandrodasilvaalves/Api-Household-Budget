using Flunt.Notifications;
using Household.Budget.Contracts.Helpers;

namespace Household.Budget.Contracts.Errors
{
    public static class CommonErrors
    {
        public static Notification UNEXPECTED_ERROR => new Notification("UNEXPECTED_ERROR", "An unexpected error has occurred.");
        public static Notification MAX_PAGE_SIZE = new("MAX_PAGE_SIZE", "Page size cannot be greater than 50.");
        public static Notification NO_CONTENT => new("NO_CONTENT", "No content.");
        public static Notification NOT_FOUND => new("NOT_FOUND", "Resource not found.");
        public static Notification INVALID_YEAR => new("INVALID_YEAR", $"The year must be between the current year minus {Year.Min} and the current year plus {Year.Max}.");
        public static Notification INVALID_MONTH => new("INVALID_MONTH", "The month must be between 1 and 12.");
    }
}