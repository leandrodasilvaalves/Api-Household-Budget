namespace Household.Budget;

public interface IGenerateAccessTokenRequestHandler
{
    Task<GenerateAccessTokenResponse> Handle(GenerateAccessTokenRequest request, CancellationToken cancellationToken);
}
