using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Categories.ImportCategorySeed;

using MassTransit;
namespace Household.Budget.Api.HostedServices;

public class ImportCategoriesDataSeedService : BackgroundService
{
    private readonly ILogger<ImportCategoriesDataSeedService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IBus _bus;
    private readonly IImportedSeedConfigRespository _respository;

    public ImportCategoriesDataSeedService(
                                  ILogger<ImportCategoriesDataSeedService> logger,
                                  IConfiguration configuration,
                                  IBus bus,
                                  IImportedSeedConfigRespository respository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        _respository = respository ?? throw new ArgumentNullException(nameof(respository));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_configuration.GetValue<bool>("Seed:Categories:Enabled") is true &&
            (await _respository.CategoriesHasBeenImportedAsync(stoppingToken) is false))
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
            
            var seedConfig = await _respository.GetAsync(stoppingToken) ??  new();
            seedConfig.UpdateCategoryConfig();
            await _respository.SaveAsync(seedConfig, stoppingToken);
            
            _logger.LogInformation("All categories data seed was sended to import process.");
        }
    }
}
