namespace Household.Budget.Api.HostedServices.ChainedServices;

public class MainBackgroundService : BackgroundService
{
    private readonly IBackgroundService _next;
    private readonly ILogger<MainBackgroundService> _logger;

    public MainBackgroundService(IBackgroundService next, ILogger<MainBackgroundService> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Main background service is starting.");

        await _next.ExecuteAsync(stoppingToken);

        _logger.LogInformation("Main background service has finished.");
    }
}