using Household.Budget.Domain.Entities;

namespace Household.Budget;

public class ChangeUserPasswordRequest : Request
{
    public ChangeUserPasswordRequest(string currentPassword,
                                     string newPassword,
                                     string confirmNewPassword)
    {
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
        ConfirmNewPassword = confirmNewPassword;
    }

    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }

    public override void Validate() =>
        AddNotifications(new ChangeUserPasswordRequestContract(this));    

    public ChangeUserPasswordViewModel ToViewModel(AppIdentityUser user) => new(user);
}

