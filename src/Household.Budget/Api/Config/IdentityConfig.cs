using Microsoft.AspNetCore.Identity;

namespace Household.Budget.Api.Config;

public static class IdentityConfig
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration config)
    {
        var options = config.GetSection("Identity").Get<IdentityOptions>();
        services.Configure<IdentityOptions>(opt => opt = options ?? DefaultOptions());
        return services;
    }

    private static IdentityOptions DefaultOptions() => new()
    {
        Password = new()
        {
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true,
            RequiredLength = 6,
            RequiredUniqueChars = 1,
        },
        User = new()
        {
            RequireUniqueEmail = true,
            AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
        },
        Lockout = new()
        {
            DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
            MaxFailedAccessAttempts = 5,
            AllowedForNewUsers = true,
        }
    };
}