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
        services.AddSingleton<IImportedSeedConfigRespository, ImportedSeedConfigRespository>();
        services.AddSingleton<IMonthlyBudgeRepository, MonthlyBudgeRepository>();
        return services;
    }

    public static void AddIdentityProvider(this IServiceCollection services, IConfiguration config)
    {
        var mongoConfig = config.GetSection(MongoConfig.SectionName).Get<MongoConfig>() ?? new();
        services.AddIdentity<AppIdentityUser, MongoIdentityRole<string>>()
                .AddMongoDbStores<AppIdentityUser, MongoIdentityRole<string>, string>(mongoConfig.ConnectionString, mongoConfig.DatabaseName)
                .AddDefaultTokenProviders();
    }
}
