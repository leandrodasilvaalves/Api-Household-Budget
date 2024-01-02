using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserHandler : IRegisterUserHandler
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public RegisterUserHandler(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<RegisterUserResponse> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var appUser = request.ToModel();
        var result = await _userManager.CreateAsync(appUser, request.Password);

        return result.Succeeded ?
            new RegisterUserResponse(request.ToViewModel()) :
            new RegisterUserResponse(result.Errors.Select(e =>
                new Notification($"IDENTITY_{e.Code.ToUpperSnakeCase()}", e.Description)));
    }
}