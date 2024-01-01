using System.Net;

using Household.Budget.Infra.Data.Context;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Household.Budget.Api.HealthCheck
{
    public class RavenDbHealthCheck : IHealthCheck
    {
        private readonly RavenConfig _ravenConfig;

        public RavenDbHealthCheck(ConfigurationManager configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _ravenConfig = configuration.GetSection(RavenConfig.SectionName).Get<RavenConfig>() ?? new();
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var uri = new Uri(_ravenConfig?.Urls?.FirstOrDefault() ?? "");
                using var httpClient = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await httpClient.SendAsync(request, cancellationToken);
                
                return response.StatusCode == HttpStatusCode.OK
                  ? HealthCheckResult.Healthy("RavenDb is up and running.")
                  : HealthCheckResult.Unhealthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}