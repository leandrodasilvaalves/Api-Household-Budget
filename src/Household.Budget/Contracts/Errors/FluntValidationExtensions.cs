using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;
using Flunt.Validations;

using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;

namespace Household.Budget;

[ExcludeFromCodeCoverage]
public static class FluntValidationExtensions
{
    public static Contract<T> IsNotNullOrEmpty<T>(this Contract<T> contract, string val, Notification notification)
    {
        return contract.IsNotNullOrEmpty(val, notification.Key, notification.Message);
    }

    public static Contract<T> IsGreaterOrEqualsThan<T>(this Contract<T> contract, string val, int comparer, Notification notification)
    {
        return contract.IsGreaterOrEqualsThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsLowerOrEqualsThan<T>(this Contract<T> contract, string val, int comparer, Notification notification)
    {
        return contract.IsLowerOrEqualsThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsValidModelTypeForUser<T>(this Contract<T> contract, ModelType modelType, IEnumerable<string> userClaims, Notification notification)
    {
        if (modelType == ModelType.SYSTEM)
        {
            if (userClaims?.Contains(IdentityClaims.ADMIN_WRITER) is false)
            {
                contract.AddNotification(notification);
            }
        }
        return contract;
    }

    public static Contract<T> Matches<T>(this Contract<T> contract, string val, string pattern, Notification notification)
    {
        return contract.Matches(val, pattern, notification.Key, notification.Message);
    }

    public static Contract<T> IsEmail<T>(this Contract<T> contract, string val, Notification notification)
    {
        return contract.IsEmail(val, notification.Key, notification.Message);
    }
}

