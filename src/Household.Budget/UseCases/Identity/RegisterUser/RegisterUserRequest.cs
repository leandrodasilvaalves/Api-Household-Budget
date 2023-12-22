using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserRequest : Request, IRequest<RegisterUserResponse>
{
    public RegisterUserRequest(string fullName,
                               string userName,
                               string email,
                               string password)
    {
        FullName = fullName;
        UserName = userName;
        Email = email;
        Password = password;        
    }

    public string FullName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }

    public AppIdentityUser ToModel() => new()
    {
        FullName = FullName,
        UserName = UserName,
        Email = Email,
    };

    public RegisterUserResponseViewModel ToViewModel() => new(this);

    public override void Validate() =>
        AddNotifications(new RegisterUserRequestContract(this));
}
