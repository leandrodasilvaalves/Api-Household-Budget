using Flunt.Notifications;

using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

using MediatR;

using Microsoft.AspNetCore.Identity;

namespace Household.Budget;

public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordRequest, ChangeUserPasswordResponse>
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly IMediator _mediator;

    public ChangeUserPasswordHandler(UserManager<AppIdentityUser> userManager, IMediator mediator)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ChangeUserPasswordResponse> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId) ?? new();
        // Fiz desta forma porque o método "ChangePasswordAsync" não está atualizando a senha
        // TODO: Atualizar packages relacionados a Identity e Raven e tentar utilizar o metodo correto futuramente
        // var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        var loginResult = await _mediator.Send(new LoginUserRequest { UserName = user.UserName, Password = request.CurrentPassword });
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