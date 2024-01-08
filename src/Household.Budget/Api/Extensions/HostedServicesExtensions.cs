using Household.Budget.Api.HostedServices;

namespace Household.Budget.Api.Extensions;

public static class HostedServicesExtensions
{
    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RootUserRegistrationService>();
        services.AddHostedService<ImportCategoriesDataSeedService>();
    }
}