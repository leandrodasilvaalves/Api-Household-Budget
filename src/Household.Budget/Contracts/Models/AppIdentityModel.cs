using Raven.Identity;

namespace Household.Budget.Contracts.Models.Identity;

public class AppIdentityModel : IdentityUser
{
    public const string AdminRole = "Admin";
    public const string ManagerRole = "Manager";

    public string? FullName { get; set; }
}
