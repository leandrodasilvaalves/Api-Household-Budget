using System.Text.Json;

using Household.Budget.UseCases.Identity.CreateAdminUser;


namespace Household.Budget.Api.HostedServices.ChainedServices;

public class RootUserRegistrationService : IBackgroundService
{
    private readonly ILogger<DatabaseCreatorService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IBackgroundService _next;

    public RootUserRegistrationService(ILogger<DatabaseCreatorService> logger,
                                  IConfiguration configuration,
                                  IServiceScopeFactory serviceScopeFactory,
                                  IBackgroundService next)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_configuration.GetValue<bool>("Identity:RootUser:Enabled") is true)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetService<ICreateAdminUserHandler>()
                ?? throw new ArgumentNullException(nameof(ICreateAdminUserHandler));

            var request = _configuration.GetSection("Identity:RootUser:Request").Get<CreateAdminUserRequest>();
            var response = await handler.Handle(request ?? CreateAdminUserRequest.DefaultAdminUser(), stoppingToken);
            if (response.IsSuccess)
            {
                _logger.LogInformation("Root user was created successfully.");
            }
            else
            {
                _logger.LogError("Root user wasn't created. Causes: {0}", JsonSerializer.Serialize(response));
            }
        }
        await _next.ExecuteAsync(stoppingToken);
    }
}