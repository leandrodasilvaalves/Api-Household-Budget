using MediatR;

namespace Household.Budget;

public class LoginUserRequest : Request, IRequest<LoginUserResponse>
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";

    public override void Validate() =>
        AddNotifications(new LoginUserRequestContract(this));
}
