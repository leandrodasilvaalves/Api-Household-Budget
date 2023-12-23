using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;

namespace Household.Budget;

[ExcludeFromCodeCoverage]
public partial class IdentityKnownErrors
{
    public static Notification USER_ID_IS_REQUIRED = new("USER_ID_IS_REQUIRED", "The x-user-id header is required");
    public static Notification FULL_NAME_ID_IS_REQUIRED = new("FULL_NAME_ID_IS_REQUIRED", "Full name is required");
    public static Notification FULL_NAME_MIN_LENGTH = new("FULL_NAME_MIN_LENGTH", "Full name must have at least 3 characters");
    public static Notification FULL_NAME_MAX_LENGTH = new("FULL_NAME_MAX_LENGTH", "Full name must be a maximum of 80 characteres");
    public static Notification USER_NAME_IS_REQUIRED = new("USER_NAME_IS_REQUIRED", "User name is required");
    public static Notification USER_NAME_MIN_LENGTH = new("USER_NAME_MIN_LENGTH", "User name must have at least 3 characters");
    public static Notification USER_NAME_MAX_LENGTH = new("USER_NAME_MAX_LENGTH", "User name must be a maximum of 25 characteres");
    public static Notification USER_NAME_MUST_HAVE_VALID_FORMAT = new("USER_NAME_MUST_HAVE_VALID_FORMAT", "User name should contain only characters and numbers");
    public static Notification EMAIL_IS_REQUIRED = new("EMAIL_IS_REQUIRED", "E-mail is required");
    public static Notification EMAIL_IS_INVALID = new("EMAIL_IS_INVALID", "E-mail is invalid");
    public static Notification PASSWORD_IS_REQUIRED = new("PASSWORD_IS_REQUIRED", "Password is required");

    //login
    public static Notification USER_IS_LOCKED_OUT = new("USER_IS_LOCKED_OUT", "Your user is locked out");
    public static Notification USER_IS_NOT_ALLOWED = new("USER_IS_NOT_ALLOWED", "Your user is not allowed to sign in");
    public static Notification TWO_FACTOR_IS_REQUIRED = new("TWO_FACTOR_IS_REQUIRED", "2FA is required");
    public static Notification BAD_USER_NAME_OR_PASSWORD = new("BAD_USER_NAME_OR_PASSWORD", "Bad user name or password");
    
    public static Notification CURRENT_PASSWORD_IS_REQUIRED = new("CURRENT_PASSWORD_IS_REQUIRED", "Current password is required");
    public static Notification NEW_PASSWORD_IS_REQUIRED = new("NEW_PASSWORD_IS_REQUIRED", "New password is required");
    public static Notification CONFIRM_NEW_PASSWORD_IS_REQUIRED = new("CONFIRM_NEW_PASSWORD_IS_REQUIRED", "Confirm new password is required");
    public static Notification NEW_PASSOWRD_MUST_BE_EQUAL_TO_CONFIRM_NEW_PASSWORD = new("NEW_PASSOWRD_MUST_BE_EQUAL_TO_CONFIRM_NEW_PASSWORD", "New password must be equal to confirm new password");
}
