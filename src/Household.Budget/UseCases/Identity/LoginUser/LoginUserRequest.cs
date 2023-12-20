using Flunt.Notifications;

using MediatR;

namespace Household.Budget;

public class LoginUserRequest : Notifiable<Notification>, IRequest<LoginUserResponse>
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}
