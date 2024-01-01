using Household.Budget.UseCases.Categories.ImportCategorySeed;

using MassTransit;
namespace Household.Budget.Api.HostedServices.ChainedServices;

public class ImportCategoriesDataSeedService : IBackgroundService
{
    private readonly ILogger<DatabaseCreatorService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IBus _bus;

    public ImportCategoriesDataSeedService(
                                  ILogger<DatabaseCreatorService> logger,
                                  IConfiguration configuration,
                                  IBus bus)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
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
