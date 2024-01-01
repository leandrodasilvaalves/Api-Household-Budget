namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public interface ICreateAdminUserHandler
{
    Task<CreateAdminUserResponse> Handle(CreateAdminUserRequest request, CancellationToken cancellationToken);
}
