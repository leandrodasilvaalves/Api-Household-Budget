using AspNetCore.Identity.MongoDbCore.Models;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data;

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
        services.AddSingleton(typeof(IData<>), typeof(Data<>));
        services.AddSingleton<ICategoryData, CategoryData>();
        services.AddSingleton<ISubcategoryData, SubcategoryData>();
        services.AddSingleton<ITransactionData, TransactionData>();
        services.AddSingleton<IImportedSeedConfigData, ImportedSeedConfigData>();
        services.AddSingleton<IMonthlyBudgetData, MonthlyBudgetData>();
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
