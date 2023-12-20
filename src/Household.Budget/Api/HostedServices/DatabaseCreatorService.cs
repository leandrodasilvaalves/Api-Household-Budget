
using Household.Budget.Contracts.Data;

namespace Household.Budget;

public class DatabaseCreatorService : BackgroundService
{
    private readonly IDatabaseCreator _database;
    private ILogger<DatabaseCreatorService> _logger;

    public DatabaseCreatorService(IDatabaseCreator database, ILogger<DatabaseCreatorService> logger)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _database.EnsureDatabaseIsCreated();
        _logger.LogInformation("Database creator was executed successfully.");
        return Task.CompletedTask;
    }
}
