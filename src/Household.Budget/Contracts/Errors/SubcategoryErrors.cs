using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors;

[ExcludeFromCodeCoverage]
public class SubcategoryErrors
{
    public static Notification SUBCATEGORY_NAME_IS_REQUIRED = new("SUBCATEGORY_NAME_IS_REQUIRED", "Subcategory name is required");
    public static Notification SUBCATEGORY_NAME_MIN_LENGTH = new("SUBCATEGORY_NAME_MIN_LENGTH", "Subcategory name must have at least 3 characters");
    public static Notification SUBCATEGORY_NAME_MAX_LENGTH = new("SUBCATEGORY_NAME_MAX_LENGTH", "Subcategory name must be a maximum of 25 characters");
    public static Notification SUBCATEGORY_NOT_FOUND = new("SUBCATEGORY_NOT_FOUND", "Requested Subcategory not found");
    public static Notification SUBCATEGORY_PAGE_SIZE_MAX = new("SUBCATEGORY_PAGE_SIZE_MAX", "Page size cannot be greater than 50");
}