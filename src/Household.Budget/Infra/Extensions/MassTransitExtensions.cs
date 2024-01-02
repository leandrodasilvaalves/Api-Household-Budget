using System.Security.Authentication;

using Household.Budget.Infra.Consumers;
using Household.Budget.Infra.Consumers.Category;
using Household.Budget.Infra.Consumers.Subcategory;

using MassTransit;

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

            bus.UsingRabbitMq((context, cfg) =>
            {
                var uri = new Uri(config.Cluster);
                cfg.Host(uri, host =>
                {
                    host.Username(config.Username);
                    host.Password(config.Password);
                    host.UseCluster(cluster =>
                    config.Hosts.ForEach(node => cluster.Node(node)));
                    if (config.UseSSL) { host.UseSsl(s => s.Protocol = SslProtocols.Tls12); }
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

                cfg.ConfigureEndpoints(context);
            });
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
    public bool UseSSL { get; set; } = false;
}