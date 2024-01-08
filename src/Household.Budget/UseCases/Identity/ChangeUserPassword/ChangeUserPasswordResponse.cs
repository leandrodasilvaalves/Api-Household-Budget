using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget;

public class ChangeUserPasswordResponse : AbstractResponse<ChangeUserPasswordViewModel>
{
    public ChangeUserPasswordResponse(ChangeUserPasswordViewModel data) : base(data) { }

    public ChangeUserPasswordResponse(IEnumerable<Notification> errors) : base(errors) { }
    public ChangeUserPasswordResponse(Notification notification) : base(notification) { }

    protected override Response NotFoundError() =>
        new (IdentityErrors.BAD_USER_NAME_OR_PASSWORD);
}



public class ChangeUserPasswordViewModel(AppIdentityUser user)
{
    public string FullName { get; } = user.FullName ?? "";
    public string UserName { get; } = user.UserName ?? "";
    public string Email { get; } = user.Email ?? "";

}
