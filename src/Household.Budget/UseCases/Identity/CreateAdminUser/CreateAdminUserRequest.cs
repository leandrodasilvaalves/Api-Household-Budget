using Household.Budget.Contracts.Models;

using MediatR;
using Household.Budget.Contracts.Constants;

namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public class CreateAdminUserRequest : Request, IRequest<CreateAdminUserResponse>
{
    public CreateAdminUserRequest(string fullName,
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
        AppIdentityUser model = new()
        {
            FullName = FullName,
            UserName = UserName,
            Email = Email,
        };
        model.Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_READER));
        model.Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_WRITER));
        model.Claims.Add(new AppIdentityUserClaim(IdentityClaims.ADMIN_READER));
        model.Claims.Add(new AppIdentityUserClaim(IdentityClaims.ADMIN_WRITER));
        return model;
    }

    public CreateAdminUserResponseViewModel ToViewModel(string userId) => new(userId, this);

    public override void Validate() =>
        AddNotifications(new CreateAdminUserRequestContract(this));

    public static CreateAdminUserRequest DefaultAdminUser() 
        => new("root user", "root", "root@localhost", "passWord@123");
}