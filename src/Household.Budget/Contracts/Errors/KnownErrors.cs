namespace Household.Budget.Contracts.Errors;

public class KnownErrors
{
    public static Error USER_ID_IS_REQUIRED = new Error("USER_ID_IS_REQUIRED", "The x-user-id header is required");        
}