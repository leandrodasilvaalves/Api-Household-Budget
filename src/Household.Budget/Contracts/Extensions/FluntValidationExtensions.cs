using System.Diagnostics.CodeAnalysis;

using Flunt.Notifications;
using Flunt.Validations;

using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Extensions;

[ExcludeFromCodeCoverage]
public static class FluntValidationExtensions
{
    public static Contract<T> AreEquals<T>(this Contract<T> contract, string value, string comparer, Notification notification)
    {
        return contract.AreEquals(value, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsNotNullOrEmpty<T>(this Contract<T> contract, string val, Notification notification)
    {
        return contract.IsNotNullOrEmpty(val, notification.Key, notification.Message);
    }    

    public static Contract<T> IsNotNull<T>(this Contract<T> contract, object? val, Notification notification)
    {
        return contract.IsNotNull(val, notification.Key, notification.Message);
    }

    public static Contract<T> IsNotNullOrWhiteSpace<T>(this Contract<T> contract, string? val, Notification notification)
    {
        return contract.IsNotNullOrWhiteSpace(val, notification.Key, notification.Message);
    }

    public static Contract<T> IsGreaterOrEqualsThan<T>(this Contract<T> contract, string val, int comparer, Notification notification)
    {
        return contract.IsGreaterOrEqualsThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsGreaterThan<T>(this Contract<T> contract, int val, int comparer, Notification notification)
    {
        return contract.IsGreaterThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsGreaterThan<T>(this Contract<T> contract, decimal val, decimal comparer, Notification notification)
    {
        return contract.IsGreaterThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsLowerOrEqualsThan<T>(this Contract<T> contract, string val, int comparer, Notification notification)
    {
        return contract.IsLowerOrEqualsThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsLowerOrEqualsThan<T>(this Contract<T> contract, int val, int comparer, Notification notification)
    {
        return contract.IsLowerOrEqualsThan(val, comparer, notification.Key, notification.Message);
    }

    public static Contract<T> IsValidModelTypeForUser<T>(this Contract<T> contract, ModelOwner ownerType, IEnumerable<string> userClaims, Notification notification)
    {
        if (ownerType == ModelOwner.SYSTEM)
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

    public static Contract<T> IsNotDefault<T>(this Contract<T> contract, Guid value, Notification notification)
    {
        if (value == default)
        {
            contract.AddNotification(notification);
        }
        return contract;
    }

    public static Contract<T> IsSatified<T>(this Contract<T> contract, T obj, Func<T, bool> firstCodition, Func<T, bool> secondCondition, Notification notification)
    {
        if (firstCodition(obj) is true & secondCondition(obj) is false)
        {
            contract.AddNotification(notification);
        }
        return contract;
    }

    public static Contract<T> IsTrue<T>(this Contract<T> contract, bool? value, Notification notification)
    {
        return contract.IsTrue(value ?? false, notification.Key, notification.Message);
    }

    public static Contract<T> IsBetween<T>(this Contract<T> contract, int val, int start, int end, Notification notification)
    {
        return contract.IsBetween(val, start, end, notification.Key, notification.Message);
    }
}
