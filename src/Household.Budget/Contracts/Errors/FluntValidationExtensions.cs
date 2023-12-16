using Flunt.Notifications;
using Flunt.Validations;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;

namespace Household.Budget;

public static class FluntValidationExtensions
{
    public static Contract<T> IsNotNullOrEmpty<T>(this Contract<T> contract, string val, Notification notification)
    {
        return contract.IsNotNullOrEmpty(val, notification.Key, notification.Message);
    }

    public static Contract<T> IsGreaterThan<T>(this Contract<T> contract, string val, int comparer, Notification notification)
    {
        return contract.IsGreaterThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsLowerThan<T>(this Contract<T> contract, string val, int comparer, Notification notification)
    {
        return contract.IsLowerThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsValidModelTypeForUser<T>(this Contract<T> contract, ModelType modelType, string userClaim)
    {
        if (modelType == ModelType.SYSTEM)
        {
            contract.AreEquals(UserClaims.ADMIN_WRITER, userClaim,
                KnownErrors.CATEGORY_TYPE_FORBIDDEN_FOR_USER.Key,
                KnownErrors.CATEGORY_TYPE_FORBIDDEN_FOR_USER.Message);
        }
        return contract;
    }
}

