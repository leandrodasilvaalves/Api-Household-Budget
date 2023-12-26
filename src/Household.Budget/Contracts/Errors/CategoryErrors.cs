using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors;

[ExcludeFromCodeCoverage]
public class CategoryErrors
{
    public static Notification CATEGORY_NAME_IS_REQUIRED = new("CATEGORY_NAME_IS_REQUIRED", "Category name is required");
    public static Notification CATEGORY_NAME_MIN_LENGTH = new("CATEGORY_NAME_MIN_LENGTH", "Category name must have at least 3 characters");
    public static Notification CATEGORY_NAME_MAX_LENGTH = new("CATEGORY_NAME_MAX_LENGTH", "Category name must be a maximum of 25 characters");
    public static Notification CATEGORY_OWNER_FORBIDDEN_FOR_USER = new("CATEGORY_OWNER_FORBIDDEN_FOR_USER", "Only administrator users can register or update categories for the SYSTEM owner");
    public static Notification CATEGORY_NOT_FOUND = new("CATEGORY_NOT_FOUND", "Requested category not found");
}
