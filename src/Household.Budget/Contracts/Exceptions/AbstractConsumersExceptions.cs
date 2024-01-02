using System.Text.Json;

namespace Household.Budget.Contracts.Exceptions;

public abstract class AbstractConsumersExceptions<TResponse> : Exception
{
    private static readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    protected AbstractConsumersExceptions(TResponse response)
        : base(JsonSerializer.Serialize(response, _options)) { }
}