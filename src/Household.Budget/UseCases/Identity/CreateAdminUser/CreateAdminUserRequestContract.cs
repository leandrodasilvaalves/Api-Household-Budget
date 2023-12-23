using Flunt.Validations;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public class CreateAdminUserRequestContract : Contract<CreateAdminUserRequest>
{
    public CreateAdminUserRequestContract(CreateAdminUserRequest request)
    {
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
}