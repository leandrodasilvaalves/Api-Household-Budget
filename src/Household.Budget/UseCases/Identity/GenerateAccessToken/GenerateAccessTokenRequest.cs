using MediatR;

namespace Household.Budget;

public class GenerateAccessTokenRequest(string userName) : IRequest<GenerateAccessTokenResponse>
{
    public string UserName { get; } = userName;
}
