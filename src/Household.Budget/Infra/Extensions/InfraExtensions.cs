using AspNetCore.Identity.MongoDbCore.Models;

using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.Infra.Extensions;

public static class InfraExtensions
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        services.AddMongo(config);
        services.AddRepositories();
        services.AddIdentityProvider(config);
        services.AddMassTransit(config);
    }

    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MongoConfig>(config.GetSection(MongoConfig.SectionName));
        services.AddSingleton(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<ISubcategoryRepository, SubcategoryRepository>();
        services.AddSingleton<ITransactionRepository, TransactionRepository>();
        return services;
    }

    public static void AddIdentityProvider(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetSection($"{MongoConfig.SectionName}:ConnectionString").Get<string>();
        services.AddIdentity<AppIdentityUser, IdentityRole>()
                .AddMongoDbStores<AppIdentityUser, MongoIdentityRole<string>, string>(connectionString, "Identity")
                .AddDefaultTokenProviders();
    }
}
