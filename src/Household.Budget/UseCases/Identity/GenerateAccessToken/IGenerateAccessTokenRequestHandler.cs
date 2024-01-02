namespace Household.Budget;

public interface IGenerateAccessTokenRequestHandler
{
    Task<GenerateAccessTokenResponse> HandleAsync(GenerateAccessTokenRequest request, CancellationToken cancellationToken);
}
