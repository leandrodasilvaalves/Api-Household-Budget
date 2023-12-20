using Raven.Identity;

namespace Household.Budget.Contracts.Models;

public class AppIdentityUser : IdentityUser
{
    public const string AdminRole = "Admin";
    public const string ManagerRole = "Manager";

    public AppIdentityUser() => Id = $"{Guid.NewGuid()}";
    public string? FullName { get; set; }
}
