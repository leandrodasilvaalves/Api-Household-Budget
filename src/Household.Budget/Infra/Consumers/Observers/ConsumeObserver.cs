using System.Text.Json;

using Household.Budget.Api.HealthCheck;
using Household.Budget.Contracts.Constants;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Observers;

public class ConsumeObserver : IConsumeObserver
{
    private readonly ILogger<ConsumeObserver> _logger;
    private readonly ImportSeedHealthCheck _importSeedHealthCheck;


    public ConsumeObserver(ILogger<ConsumeObserver> logger,
                                     ImportSeedHealthCheck importSeedHealthCheck)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _importSeedHealthCheck = importSeedHealthCheck ?? throw new ArgumentNullException(nameof(importSeedHealthCheck));
    }

    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        _logger.LogError(JsonSerializer.Serialize(new { context.Message, Heanders = context.Headers.GetAll(), Exception = exception.Message }));
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        if (context.Headers.TryGetHeader(KnownHeaders.IMORT_PROCESS, out _))
        {
            _importSeedHealthCheck.ReceivedMessage();
        }
        return Task.CompletedTask;
    }

    public Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }
}