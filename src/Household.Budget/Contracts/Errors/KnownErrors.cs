using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors;

[ExcludeFromCodeCoverage]
public class KnownErrors
{
    public static Notification USER_ID_IS_REQUIRED = new("USER_ID_IS_REQUIRED", "The x-user-id header is required");        
    public static Notification CATEGORY_NAME_IS_REQUIRED = new("CATEGORY_NAME_IS_REQUIRED", "Category name is required");        
    public static Notification CATEGORY_NAME_MIN_LENGTH = new("CATEGORY_NAME_MIN_LENGTH", "The category name must have at least 3 characters");        
    public static Notification CATEGORY_NAME_MAX_LENGTH = new("CATEGORY_NAME_MAX_LENGTH", "The category name must be a maximum of 25 characters");        
    public static Notification CATEGORY_TYPE_FORBIDDEN_FOR_USER = new("CATEGORY_TYPE_FORBIDDEN_FOR_USER", "Only administrator users can register or update categories for the SYSTEM type");        
}