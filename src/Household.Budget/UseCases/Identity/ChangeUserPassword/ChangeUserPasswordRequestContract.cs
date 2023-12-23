using Flunt.Validations;

namespace Household.Budget;

public class ChangeUserPasswordRequestContract : Contract<ChangeUserPasswordRequest>
{
    public ChangeUserPasswordRequestContract(ChangeUserPasswordRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.CurrentPassword, IdentityKnownErrors.CURRENT_PASSWORD_IS_REQUIRED)
            .IsNotNullOrEmpty(request.NewPassword, IdentityKnownErrors.NEW_PASSWORD_IS_REQUIRED)
            .IsNotNullOrEmpty(request.ConfirmNewPassword, IdentityKnownErrors.CONFIRM_NEW_PASSWORD_IS_REQUIRED)
            .AreEquals(request.NewPassword, request.ConfirmNewPassword, IdentityKnownErrors.NEW_PASSOWRD_MUST_BE_EQUAL_TO_CONFIRM_NEW_PASSWORD);
    }
}

