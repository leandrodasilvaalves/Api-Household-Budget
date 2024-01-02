namespace Household.Budget;

public class GenerateAccessTokenRequest(string userName)
{
    public string UserName { get; } = userName;
}
