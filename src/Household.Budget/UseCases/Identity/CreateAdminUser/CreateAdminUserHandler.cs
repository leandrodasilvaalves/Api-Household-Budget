using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public class CreateAdminUserHandler : IRequestHandler<CreateAdminUserRequest, CreateAdminUserResponse>
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public CreateAdminUserHandler(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<CreateAdminUserResponse> Handle(CreateAdminUserRequest request, CancellationToken cancellationToken)
    {
        var appUser = request.ToModel();
        var result = await _userManager.CreateAsync(appUser, request.Password);

        return result.Succeeded ?
            new CreateAdminUserResponse(request.ToViewModel()) :
            new CreateAdminUserResponse(result.Errors.Select(e =>
                new Notification($"IDENTITY_{e.Code.ToUpperSnakeCase()}", e.Description)));
    }
}