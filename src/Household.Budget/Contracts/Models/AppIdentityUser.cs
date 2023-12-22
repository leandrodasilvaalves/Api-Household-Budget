using Raven.Identity;

namespace Household.Budget.Contracts.Models;

public class AppIdentityUser : IdentityUser
{
    public AppIdentityUser() => Id = $"{Guid.NewGuid()}";
    public string? FullName { get; set; }
}

public class AppIdentityUserClaim : IdentityUserClaim
{
    public AppIdentityUserClaim(string claim)
    {
        ClaimType = claim;
        ClaimValue = claim;
    }
}
