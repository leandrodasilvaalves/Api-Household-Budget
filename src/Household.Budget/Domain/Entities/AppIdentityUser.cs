using AspNetCore.Identity.MongoDbCore.Models;

using Household.Budget.Contracts.Constants;
using Household.Budget.UseCases.Identity.CreateAdminUser;
using Household.Budget.UseCases.Identity.RegisterUser;

namespace Household.Budget.Domain.Entities;

public class AppIdentityUser : MongoIdentityUser<string>
{
    public AppIdentityUser() => Id = $"{Guid.NewGuid()}";
    public string? FullName { get; set; }

    public void CreateUser(RegisterUserRequest request)
    {
        FullName = request.FullName;
        UserName = request.UserName;
        Email = request.Email;
        Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_READER));
        Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_WRITER));
    }

    public void CreateAdminUser(CreateAdminUserRequest request)
    {
        Id = request.UserId;
        FullName = request.FullName;
        UserName = request.UserName;
        Email = request.Email;
        Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_READER));
        Claims.Add(new AppIdentityUserClaim(IdentityClaims.USER_WRITER));
        Claims.Add(new AppIdentityUserClaim(IdentityClaims.ADMIN_READER));
        Claims.Add(new AppIdentityUserClaim(IdentityClaims.ADMIN_WRITER));
    }
}

public class AppIdentityUserClaim : MongoClaim
{
    public AppIdentityUserClaim(string claim)
    {
        Type = claim;
        Value = claim;
    }
}