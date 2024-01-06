using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget;

public class LoginUserResponse : AbstractResponse<object>
{
    public LoginUserResponse(GenerateAccessTokenResponse data) : base(data) { }

    public LoginUserResponse(IEnumerable<Notification> errors) : base(errors) { }
    public LoginUserResponse(Notification error) : base(error) { }
}