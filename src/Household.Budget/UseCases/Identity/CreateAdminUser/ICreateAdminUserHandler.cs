namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public interface ICreateAdminUserHandler
{
    Task<CreateAdminUserResponse> HandleAsync(CreateAdminUserRequest request, CancellationToken cancellationToken);
}
