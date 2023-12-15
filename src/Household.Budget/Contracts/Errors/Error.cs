using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors;

public class Error(string key, string message)
    : Notification(key, message)
{ }