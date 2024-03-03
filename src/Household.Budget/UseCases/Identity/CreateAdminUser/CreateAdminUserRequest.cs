namespace Household.Budget.UseCases.Identity.CreateAdminUser;

public class CreateAdminUserRequest : Request
{
    public CreateAdminUserRequest(string userId,
                                  string fullName,
                                  string userName,
                                  string email,
                                  string password)
    {
        UserId = userId;
        FullName = fullName;
        UserName = userName;
        Email = email;
        Password = password;
    }

    public string FullName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }

    public CreateAdminUserResponseViewModel ToViewModel(string userId) => new(userId, this);

    public override void Validate() =>
        AddNotifications(new CreateAdminUserRequestContract(this));

    public static CreateAdminUserRequest DefaultAdminUser()
        => new($"{Guid.NewGuid()}", "root user", "root", "root@localhost", "passWord@123");
}