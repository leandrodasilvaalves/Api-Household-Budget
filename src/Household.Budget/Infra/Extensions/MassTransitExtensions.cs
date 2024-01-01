using System.Security.Authentication;

using Household.Budget.Infra.Consumers;
using Household.Budget.Infra.Consumers.Category;

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
                    endpoint.ConfigureConsumer<SubcategoryWasCreatedConsumer>(context);
                    endpoint.ConfigureConsumer<SubCategoryWasExcludedConsumer>(context);
                    endpoint.ConfigureConsumer<SubcategoryChangedCategoryConsumer>(context);
                });

                cfg.ReceiveEndpoint("subcategories.requests", endpoint =>
                {
                    endpoint.ConfigureConsumer<ImportCategorySeedRequestConsumer>(context);
                });

                cfg.ConfigureEndpoints(context);
            });
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