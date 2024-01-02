namespace Household.Budget;

public class LoginUserRequest : Request
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";

    public override void Validate() =>
        AddNotifications(new LoginUserRequestContract(this));
}
