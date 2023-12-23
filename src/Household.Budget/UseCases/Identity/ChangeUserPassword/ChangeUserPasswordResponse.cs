using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget;

public class ChangeUserPasswordResponse : Response<ChangeUserPasswordViewModel>
{
    public ChangeUserPasswordResponse(ChangeUserPasswordViewModel data) : base(data) { }

    public ChangeUserPasswordResponse(IEnumerable<Notification> errors) : base(errors) { }
    public ChangeUserPasswordResponse(Notification notification) : base(notification) { }
}



public class ChangeUserPasswordViewModel(AppIdentityUser user)
{
    public string FullName { get; } = user.FullName ?? "";
    public string UserName { get; } = user.UserName;
    public string Email { get; } = user.Email;

}
