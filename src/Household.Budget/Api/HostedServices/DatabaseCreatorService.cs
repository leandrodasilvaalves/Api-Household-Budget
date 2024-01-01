using System.Text.Json;

using Household.Budget.Contracts.Data;
using Household.Budget.UseCases.Categories.ImportCategorySeed;
using Household.Budget.UseCases.Identity.CreateAdminUser;

using MassTransit;

using MediatR;

namespace Household.Budget.Api.HostedServices;

public class DatabaseCreatorService : BackgroundService
{
    private readonly IDatabaseCreator _database;
    private readonly ILogger<DatabaseCreatorService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IBus _bus;

    public DatabaseCreatorService(IDatabaseCreator database,
                                  ILogger<DatabaseCreatorService> logger,
                                  IConfiguration configuration,
                                  IServiceScopeFactory serviceScopeFactory,
                                  IBus bus)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await CreateDatabaseAsync(stoppingToken);
        await CreateRootUserAsync(stoppingToken);
        await ImportCategoriesDataSeedAsync(stoppingToken);
    }

    private async Task CreateDatabaseAsync(CancellationToken stoppingToken)
    {
        await _database.EnsureDatabaseIsCreatedAsync(stoppingToken);
        _logger.LogInformation("Database creator was executed successfully.");
    }

    private async Task CreateRootUserAsync(CancellationToken stoppingToken)
    {
        if (_configuration.GetValue<bool>("Identity:RootUser:Enabled") is true)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>()
                ?? throw new ArgumentNullException(nameof(IMediator));

            var request = _configuration.GetSection("Identity:RootUser:Request").Get<CreateAdminUserRequest>();
            var response = await mediator.Send(request ?? CreateAdminUserRequest.DefaultAdminUser(), stoppingToken);
            if (response.IsSuccess)
            {
                _logger.LogInformation("Root user was created successfully.");
            }
            else
                _logger.LogError("Root user wasn't created. Causes: {0}", JsonSerializer.Serialize(response));
        }
    }

    private async Task ImportCategoriesDataSeedAsync(CancellationToken stoppingToken)
    {
        if (_configuration.GetValue<bool>("Seed:Categories:Enabled") is true)
        {
            var rootUserId = _configuration.GetSection("Identity:RootUser:Request:UserId").Get<string>();
            if (string.IsNullOrWhiteSpace(rootUserId))
                throw new InvalidOperationException("Root user was not found. Please, create it first.");

            _logger.LogInformation("Categories import has been initialized");

            var categories = _configuration.GetSection("Seed:Categories:Data").Get<List<ImportCategorySeedRequest>>() ?? [];

            var tasks = new List<Task>();
            var sendEndpoint = await _bus.GetPublishSendEndpoint<ImportCategorySeedRequest>();            
            categories.ForEach(request => tasks.Add(sendEndpoint.Send(request.WithRootUserId(rootUserId), stoppingToken)));
            await Task.WhenAll(tasks);
            _logger.LogInformation("Categories data seed was imported successfully.");
        }
    }
}