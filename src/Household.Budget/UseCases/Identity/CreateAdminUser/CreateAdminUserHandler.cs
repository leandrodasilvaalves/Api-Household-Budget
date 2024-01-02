using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public class CreateAdminUserHandler : ICreateAdminUserHandler
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public CreateAdminUserHandler(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<CreateAdminUserResponse> HandleAsync(CreateAdminUserRequest request, CancellationToken cancellationToken)
    {
        var appUser = request.ToModel();
        var result = await _userManager.CreateAsync(appUser, request.Password);

        return result.Succeeded ?
            new CreateAdminUserResponse(request.ToViewModel(appUser.Id ?? "")) :
            new CreateAdminUserResponse(result.Errors.Select(e =>
                new Notification($"IDENTITY_{e.Code.ToUpperSnakeCase()}", e.Description)));
    }
}