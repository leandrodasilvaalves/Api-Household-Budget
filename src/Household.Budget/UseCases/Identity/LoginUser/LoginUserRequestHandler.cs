using Flunt.Notifications;

using Household.Budget.Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget.UseCases.Identity.LoginUser;


public class LoginUserRequestHandler : ILoginUserRequestHandler
{
    private readonly SignInManager<AppIdentityUser> _signInManager;
    private readonly IGenerateAccessTokenRequestHandler _tokenHandler;

    public LoginUserRequestHandler(SignInManager<AppIdentityUser> signInManager, IGenerateAccessTokenRequestHandler tokenHandler)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
    }

    public async Task<LoginUserResponse> HandleAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
        return result.Succeeded ? await GenerateJwtToken(request, cancellationToken) : ReturnLoginUserResponseError(result);
    }

    private async Task<LoginUserResponse> GenerateJwtToken(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var accessTokenResponse = await _tokenHandler.HandleAsync(new GenerateAccessTokenRequest(request.UserName), cancellationToken);
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