using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors
{
    public static class CommonErrors
    {
        public static Notification UNEXPECTED_ERROR => new Notification("UNEXPECTED_ERROR", "An unexpected error has occurred.");
    }
}