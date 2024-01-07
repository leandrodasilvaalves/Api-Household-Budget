using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;
using Household.Budget.UseCases.Identity.LoginUser;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget;

public class ChangeUserPasswordHandler : IChangeUserPasswordHandler
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly ILoginUserRequestHandler _loginHandler;

    public ChangeUserPasswordHandler(UserManager<AppIdentityUser> userManager, ILoginUserRequestHandler loginHandler)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
    }

    public async Task<ChangeUserPasswordResponse> HandleAsync(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId) ?? new();
        // Fiz desta forma porque o método "ChangePasswordAsync" não está atualizando a senha
        // TODO: Atualizar packages relacionados a Identity e Raven e tentar utilizar o metodo correto futuramente
        // var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        var loginResult = await _loginHandler.HandleAsync(new LoginUserRequest { UserName = user.UserName ?? "", Password = request.CurrentPassword }, cancellationToken);
        if (loginResult.IsSuccess)
        {
            await _userManager.DeleteAsync(user);
            var result = await _userManager.CreateAsync(user, request.NewPassword);

            return result.Succeeded ?
                new ChangeUserPasswordResponse(request.ToViewModel(user)) :
                new ChangeUserPasswordResponse(result.Errors.Select(e =>
                    new Notification($"IDENTITY_{e.Code.ToUpperSnakeCase()}", e.Description)));
        }
        return new ChangeUserPasswordResponse(new Notification("IDENTITY_PASSWORD_MISMATCH", "Incorrect password."));
    }
}