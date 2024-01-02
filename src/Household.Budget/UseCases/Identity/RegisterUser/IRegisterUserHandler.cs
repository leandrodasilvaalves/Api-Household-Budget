namespace Household.Budget.UseCases.Identity.RegisterUser;

public interface IRegisterUserHandler
{
    Task<RegisterUserResponse> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken);
}
