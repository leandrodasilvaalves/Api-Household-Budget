using System.Text.Json.Serialization;

using Household.Budget.Api.Controllers.Filters;
using Household.Budget.Api.HealthCheck;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Extensions;

public static class ApiConfiExtensions
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddControllers(options => options.Filters.Add<AddUserClaimsFilter>())
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddEndpointsApiExplorer();
    }

    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
           .AddCheck("RavenDb", new RavenDbHealthCheck(config));
    }
}