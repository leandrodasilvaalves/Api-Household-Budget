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
            notification = IdentityErrors.USER_IS_LOCKED_OUT;
        }
        else if (result.IsNotAllowed)
        {
            notification = IdentityErrors.USER_IS_NOT_ALLOWED;
        }
        else if (result.RequiresTwoFactor)
        {
            notification = IdentityErrors.TWO_FACTOR_IS_REQUIRED;
        }
        else
        {
            notification = IdentityErrors.BAD_USER_NAME_OR_PASSWORD;
        }
        return new LoginUserResponse(notification);
    }
}