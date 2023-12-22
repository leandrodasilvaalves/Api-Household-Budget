using System.Diagnostics.CodeAnalysis;

namespace Household.Budget.Contracts.Constants;

[ExcludeFromCodeCoverage]
public class IdentityClaims
{
    public const string USER_ID = "user_id";
    public const string USER_NAME = "user_name";
    public const string USER_EMAIL = "user_email";

    public const string ADMIN_WRITER = "admin.writer";
    public const string ADMIN_READER = "admin.reader";
    public const string USER_WRITER = "user.writer";
    public const string USER_READER = "user.reader";
}
