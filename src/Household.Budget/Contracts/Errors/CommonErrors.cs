using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors
{
    public static class CommonErrors
    {
        public static Notification UNEXPECTED_ERROR => new Notification("UNEXPECTED_ERROR", "An unexpected error has occurred.");
        public static Notification MAX_PAGE_SIZE = new("MAX_PAGE_SIZE", "Page size cannot be greater than 50");
    }
}