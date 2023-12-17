using Flunt.Localization;
using Flunt.Validations;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserRequestContract : Contract<RegisterUserRequest>
{
    public RegisterUserRequestContract(RegisterUserRequest request)
    {
        ContractFullName(request.FullName);
        ContractUserName(request.UserName);
        ContractEmail(request.Email);
        ContractPassword(request.Password);
    }

    private void ContractPassword(string password)
    {
        Requires()
            .IsNotNullOrEmpty(password, IdentityKnownErrors.PASSWORD_IS_REQUIRED);
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
            .IsLowerOrEqualsThan(userName, 25, IdentityKnownErrors.USER_NAME_MIN_LENGTH)
            .Matches(userName, FluntRegexPatterns.OnlyLettersAndNumbersPatter, IdentityKnownErrors.USER_NAME_MUST_HAVE_VALID_FORMAT);
    }

    private void ContractFullName(string fullName)
    {
        Requires()
            .IsNotNullOrEmpty(fullName, IdentityKnownErrors.FULL_NAME_ID_IS_REQUIRED)
            .IsGreaterOrEqualsThan(fullName, 3, IdentityKnownErrors.FULL_NAME_MIN_LENGTH)
            .IsLowerOrEqualsThan(fullName, 80, IdentityKnownErrors.FULL_NAME_MAX_LENGTH);
    }
}