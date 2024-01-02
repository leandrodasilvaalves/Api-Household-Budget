using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

using Raven.DependencyInjection;
using Raven.Identity;

namespace Household.Budget.Infra.Extensions;

public static class InfraExtensions
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        services.AddRavenDb(config);
        services.AddIdentityProvider(config);
        services.AddMassTransit(config);
    }

    public static IServiceCollection AddRavenDb(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RavenConfig>(config.GetSection(RavenConfig.SectionName));
        services.AddSingleton<IRavenDbContext, RavenDbContext>();
        services.AddSingleton<IDatabaseCreator, RavenDbContext>();
        services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<ISubcategoryRepository, SubcategoryRepository>();
        return services;
    }

    public static void AddIdentityProvider(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddRavenDbDocStore()
            .AddRavenDbAsyncSession()
            .AddIdentity<AppIdentityUser, IdentityRole>()
            .AddRavenDbIdentityStores<AppIdentityUser, IdentityRole>();
    }
}
