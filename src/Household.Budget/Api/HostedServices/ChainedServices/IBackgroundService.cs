namespace Household.Budget.Api.HostedServices.ChainedServices;

public interface IBackgroundService
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}
