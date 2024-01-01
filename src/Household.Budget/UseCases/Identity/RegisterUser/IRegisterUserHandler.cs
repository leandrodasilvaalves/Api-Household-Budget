namespace Household.Budget.UseCases.Identity.RegisterUser;

public interface IRegisterUserHandler
{
    Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken);
}
