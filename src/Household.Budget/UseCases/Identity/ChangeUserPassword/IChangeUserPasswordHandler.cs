namespace Household.Budget;

public interface IChangeUserPasswordHandler
{
    public Task<ChangeUserPasswordResponse> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken);
}
