using Flunt.Notifications;

using Household.Budget.Contracts.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.UseCases.Identity.LoginUser;

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    private readonly SignInManager<AppIdentityUser> _signInManager;
    private readonly IMediator _mediator;

    public LoginUserRequestHandler(SignInManager<AppIdentityUser> signInManager, IMediator mediator)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        if (request.IsValid is false)
        {
            return new LoginUserResponse(request.Notifications);
        }

        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
        return result.Succeeded ? await GenerateJwtToken(request) : ReturnLoginUserResponseError(result);
    }

    private async Task<LoginUserResponse> GenerateJwtToken(LoginUserRequest request)
    {
        var accessTokenResponse = await _mediator.Send(new GenerateAccessTokenRequest(request.UserName));
        return new LoginUserResponse(accessTokenResponse);
    }

    private static LoginUserResponse ReturnLoginUserResponseError(SignInResult result)
    {
        Notification notification;
        if (result.IsLockedOut)
        {
            notification = new Notification("USER_IS_LOCKED_OUT", "Your user is locked out");
        }
        else if (result.IsNotAllowed)
        {
            notification = new Notification("USER_IS_NOT_ALLOWED", "Your user is not allowed to sign in");
        }
        else if (result.RequiresTwoFactor)
        {
            notification = new Notification("2FA_IS_REQUIRED", "2FA is required");
        }
        else
        {
            notification = new Notification("BAD_USER_NAME_OR_PASSWORD", "Bad user name or password");
        }
        return new LoginUserResponse(notification);
    }
}