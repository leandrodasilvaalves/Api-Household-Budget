using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserRequest : Request, IRequest<RegisterUserResponse>
{
    public RegisterUserRequest(string fullName,
                               string userName,
                               string email,
                               string password)
    {
        FullName = fullName;
        UserName = userName;
        Email = email;
        Password = password;        
    }

    public string FullName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }

    public AppIdentityUser ToModel()
    {
        AppIdentityUser model =  new()
        {
            FullName = FullName,
            UserName = UserName,
            Email = Email,
        };
        model.Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_READER));
        model.Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_WRITER));
        return model;
    }

    public RegisterUserResponseViewModel ToViewModel() => new(this);

    public override void Validate() =>
        AddNotifications(new RegisterUserRequestContract(this));
}
