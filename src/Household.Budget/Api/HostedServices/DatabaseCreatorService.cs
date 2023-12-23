
using System.Text.Json;

using Household.Budget.Contracts.Data;
using Household.Budget.UseCases.Identity.CreateAdminUser;

using MediatR;

namespace Household.Budget;

public class DatabaseCreatorService : BackgroundService
{
    private readonly IDatabaseCreator _database;
    private ILogger<DatabaseCreatorService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DatabaseCreatorService(IDatabaseCreator database,
                                  ILogger<DatabaseCreatorService> logger,
                                  IConfiguration configuration,
                                  IServiceScopeFactory serviceScopeFactory)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await CreateDatabaseAsync();
        await CreateRootUser(stoppingToken);
    }

    private Task CreateDatabaseAsync()
    {
        _database.EnsureDatabaseIsCreated();
        _logger.LogInformation("Database creator was executed successfully.");
        return Task.CompletedTask;
    }

    private async Task CreateRootUser(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetService<IMediator>()
                ?? throw new ArgumentNullException(nameof(IMediator));

            if (_configuration.GetValue<bool>("Identity:RootUser:Enabled") is true)
            {
                var request = _configuration.GetSection("Identity:RootUser:Request").Get<CreateAdminUserRequest>();
                var response = await mediator.Send(request ?? CreateAdminUserRequest.DefaultAdminUser(), stoppingToken);
                if (response.IsSuccess)
                    _logger.LogInformation("Root user was created successfully.");
                else
                    _logger.LogError("Root user wasn't created. Causes: {0}", JsonSerializer.Serialize(response));
            }
        }
    }
}