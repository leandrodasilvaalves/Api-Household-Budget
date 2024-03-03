using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget;

public class ChangeUserPasswordHandler : IChangeUserPasswordHandler
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public ChangeUserPasswordHandler(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<ChangeUserPasswordResponse> HandleAsync(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId) ?? new();
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        return result.Succeeded
            ? new ChangeUserPasswordResponse(request.ToViewModel(user))
            : new ChangeUserPasswordResponse(result.Errors.Select(e =>
                new Notification($"IDENTITY_{e.Code.ToUpperSnakeCase()}", e.Description)));
    }
}