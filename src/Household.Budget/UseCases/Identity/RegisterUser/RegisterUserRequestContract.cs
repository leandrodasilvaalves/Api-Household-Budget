using Flunt.Validations;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserRequestContract : Contract<RegisterUserRequest>
{
    public RegisterUserRequestContract(RegisterUserRequest request)
    {
        ContractFullName(request.FullName);
        ContractUserName(request.UserName);
        ContractEmail(request.Email);
    }

    private void ContractEmail(string email)
    {
        Requires()
            .IsNotNullOrEmpty(email, IdentityKnownErrors.EMAIL_IS_REQUIRED)
            .IsEmail(email, IdentityKnownErrors.EMAIL_IS_INVALID);
    }

    private void ContractUserName(string userName)
    {
        Requires()
            .IsGreaterOrEqualsThan(userName, 3, IdentityKnownErrors.USER_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(userName, 25, IdentityKnownErrors.USER_NAME_MIN_LENGTH);            
    }

    private void ContractFullName(string fullName)
    {
        Requires()
            .IsNotNullOrEmpty(fullName, IdentityKnownErrors.FULL_NAME_ID_IS_REQUIRED)
            .IsGreaterOrEqualsThan(fullName, 3, IdentityKnownErrors.FULL_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(fullName, 80, IdentityKnownErrors.FULL_NAME_MAX_LENGTH)
            .Matches(fullName, @"[A-Za-z\s]", IdentityKnownErrors.USER_NAME_MUST_HAVE_VALID_FORMAT);
    }
}