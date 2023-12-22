using Flunt.Notifications;

namespace Household.Budget;

public abstract class Request : Notifiable<Notification>
{
    public string? UserId { get; set; }

    public IEnumerable<string>? UserClaims { get; set; }

    public abstract void Validate();
}