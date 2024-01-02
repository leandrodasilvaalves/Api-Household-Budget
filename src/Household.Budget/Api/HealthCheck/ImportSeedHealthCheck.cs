using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Household.Budget.Api.HealthCheck;

public class ImportSeedHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ImportSeedHealthCheck> _logger;


    public ImportSeedHealthCheck(IConfiguration configuration, ILogger<ImportSeedHealthCheck> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        InactivityCheckTimer = new Timer(VerifyInactivity, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    private bool IsReady { get; set; } = false;
    private DateTime LastMessageReceivedTime { get; set; } = DateTime.UtcNow;
    private TimeSpan InactivityThreshold => TimeSpan.FromSeconds(5);
    private Timer InactivityCheckTimer { get; }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (_configuration.GetSection("Seed:Categories:Enabled").Get<bool>())
        {
            return IsReady
                ? Task.FromResult(HealthCheckResult.Healthy("Import seed data is completed."))
                : Task.FromResult(HealthCheckResult.Unhealthy("Import seed data is still running."));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Healthy("Import seed data is disabled."));
        }
    }

    public void ReceivedMessage()
    {
        LastMessageReceivedTime = DateTime.UtcNow;
    }

    private void VerifyInactivity(object state)
    {
        var elapsed = DateTime.UtcNow - LastMessageReceivedTime;
        if (elapsed >= InactivityThreshold)
        {
            InactivityCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
            IsReady = true;
            _logger.LogInformation("Inactivity of 05 seconds. \"SetReady()\" method has been invoked.");
        }
    }
}