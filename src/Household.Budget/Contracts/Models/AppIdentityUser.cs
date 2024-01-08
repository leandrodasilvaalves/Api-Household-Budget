using AspNetCore.Identity.MongoDbCore.Models;

namespace Household.Budget.Contracts.Models;

public class AppIdentityUser : MongoIdentityUser<string>
{
    public AppIdentityUser() => Id = $"{Guid.NewGuid()}";
    public string? FullName { get; set; }
}

public class AppIdentityUserClaim : MongoClaim
{
    public AppIdentityUserClaim(string claim)
    {
        Type = claim;
        Value = claim;
    }
}