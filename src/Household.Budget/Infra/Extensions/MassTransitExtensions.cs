using Household.Budget.Infra.Consumers;
using Household.Budget.Infra.Consumers.Category;
using Household.Budget.Infra.Consumers.Subcategory;

using MassTransit;
using Household.Budget.Infra.Consumers.Observers;
using Household.Budget.Infra.Consumers.Transactions;

namespace Household.Budget.Infra.Extensions;

public static class MassTransitExtensions
{
    public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection(RabbitConfig.SectionName).Get<RabbitConfig>() ?? new();        
        services.AddMassTransit(bus =>
        {
            bus.AddConsumer<SubcategoryWasCreatedConsumer>();
            bus.AddConsumer<SubCategoryWasExcludedConsumer>();
            bus.AddConsumer<SubcategoryChangedCategoryConsumer>();
            bus.AddConsumer<ImportCategorySeedRequestConsumer>();
            bus.AddConsumer<CreateSubcategorySeedRequestConsumer>();
            bus.AddConsumer<TransactionWasCreatedConsumer>();
            bus.AddConsumer<TransactionWasUpdatedConsumer>();
            bus.AddConsumer<ImportTransactionConsumer>();

            bus.UsingRabbitMq((context, cfg) =>
            {
                var uri = new Uri(config.Cluster);
                cfg.Host(uri, config.VirtualHost, host =>
                {
                    host.Username(config.Username);
                    host.Password(config.Password);
                    host.UseCluster(cluster =>
                    config.Hosts.ForEach(node => cluster.Node(node)));
                });

                cfg.ReceiveEndpoint("subcategories.notifications", endpoint =>
                {
                    endpoint.CustomConfigureConsumer<SubcategoryWasCreatedConsumer>(context);
                    endpoint.CustomConfigureConsumer<SubCategoryWasExcludedConsumer>(context);
                    endpoint.CustomConfigureConsumer<SubcategoryChangedCategoryConsumer>(context);
                });

                cfg.ReceiveEndpoint("subcategories.requests", endpoint =>
                {
                    endpoint.CustomConfigureConsumer<ImportCategorySeedRequestConsumer>(context);
                    endpoint.CustomConfigureConsumer<CreateSubcategorySeedRequestConsumer>(context);
                });

                cfg.ReceiveEndpoint("transactions.notifications", endpoint =>
                {
                    endpoint.CustomConfigureConsumer<TransactionWasCreatedConsumer>(context);
                    endpoint.CustomConfigureConsumer<TransactionWasUpdatedConsumer>(context);
                });

                cfg.ReceiveEndpoint("transactions.requests", endpoint =>
                {
                    endpoint.CustomConfigureConsumer<ImportTransactionConsumer>(context);
                    endpoint.PrefetchCount = 3;
                });

                cfg.ConfigureEndpoints(context);
            });
            services.AddConsumeObserver<ConsumeObserver>();
        });
    }

    private static void CustomConfigureConsumer<T>(this IRabbitMqReceiveEndpointConfigurator endpoint,
                                                   IBusRegistrationContext context) where T : class, IConsumer
    {
        endpoint.ConfigureConsumer<T>(context, consumer =>
        {
            consumer.UseMessageRetry(retry => retry.Interval(5, TimeSpan.FromSeconds(2)));
        });
    }
}

public class RabbitConfig
{
    public const string SectionName = "RabbitMQ";
    public string Cluster { get; set; } = "";
    public List<string> Hosts { get; set; } = [];
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string VirtualHost { get; set; } = "/";
}