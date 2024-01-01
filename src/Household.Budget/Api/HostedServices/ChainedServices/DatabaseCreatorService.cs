using Household.Budget.Contracts.Data;

namespace Household.Budget.Api.HostedServices.ChainedServices;

public class DatabaseCreatorService : IBackgroundService
{
    private readonly IDatabaseCreator _database;
    private readonly ILogger<DatabaseCreatorService> _logger;
    private readonly IBackgroundService _next;

    public DatabaseCreatorService(IDatabaseCreator database,
                                  ILogger<DatabaseCreatorService> logger,
                                  IBackgroundService next)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _database.EnsureDatabaseIsCreatedAsync(stoppingToken);
        _logger.LogInformation("Database creator was executed successfully.");

        await _next.ExecuteAsync(stoppingToken);
    }
}