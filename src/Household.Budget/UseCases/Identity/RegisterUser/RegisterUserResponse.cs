using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget.UseCases.Identity.RegisterUser;

public class RegisterUserResponse : AbstractResponse<RegisterUserResponseViewModel>
{
    public RegisterUserResponse(RegisterUserResponseViewModel response) : base(response) { }
    public RegisterUserResponse(IEnumerable<Notification> errors) : base(errors) { }
}

public class RegisterUserResponseViewModel(RegisterUserRequest request)
{
    public string FullName { get; set; } = request.FullName;
    public string UserName { get; set; } = request.UserName;
    public string Email { get; set; } = request.Email;
}