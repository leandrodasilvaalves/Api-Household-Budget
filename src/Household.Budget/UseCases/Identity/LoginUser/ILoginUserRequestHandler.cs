namespace Household.Budget.UseCases.Identity.LoginUser;

public interface ILoginUserRequestHandler
{
    Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken);
}
