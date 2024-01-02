namespace Household.Budget;

public interface IChangeUserPasswordHandler
{
    public Task<ChangeUserPasswordResponse> HandleAsync(ChangeUserPasswordRequest request, CancellationToken cancellationToken);
}
