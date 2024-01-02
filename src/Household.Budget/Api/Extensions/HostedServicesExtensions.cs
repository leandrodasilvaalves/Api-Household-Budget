using Household.Budget.Api.HostedServices.ChainedServices;

namespace Household.Budget.Api.Extensions;

public static class HostedServicesExtensions
{
    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddChainedBackgroundServices();
    }

    private static void AddChainedBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<MainBackgroundService>();
        services.AddSingleton<IBackgroundService, ImportCategoriesDataSeedService>();
        services.Decorate<IBackgroundService, RootUserRegistrationService>();
        services.Decorate<IBackgroundService, DatabaseCreatorService>();
    }
}