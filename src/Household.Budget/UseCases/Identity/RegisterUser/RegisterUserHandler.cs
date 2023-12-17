using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models.Identity;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
{
    private readonly UserManager<AppIdentityModel> _userManager;

    public RegisterUserHandler(UserManager<AppIdentityModel> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        if(request.IsValid is false)
        {
            return new RegisterUserResponse(request.Notifications);
        }
        
        var appUser = request.ToModel();
        var result = await _userManager.CreateAsync(appUser, request.Password);

        return result.Succeeded ?
            new RegisterUserResponse(request.ToViewModel()) :
            new RegisterUserResponse(result.Errors.Select(e =>
                new Notification($"IDENTITY_{e.Code.ToUpperSnakeCase()}", e.Description)));
    }
}