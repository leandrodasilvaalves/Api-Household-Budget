namespace Household.Budget.UseCases.Identity.LoginUser;

public interface ILoginUserRequestHandler
{
    Task<LoginUserResponse> HandleAsync(LoginUserRequest request, CancellationToken cancellationToken);
}
