using System.Diagnostics.CodeAnalysis;
using Household.Budget.Contracts.Helpers;

using Flunt.Notifications;

namespace Household.Budget.Contracts.Errors;

[ExcludeFromCodeCoverage]
public class TransactionErrors
{
    public static Notification CATEGORY_IS_REQUIRED = new("CATEGORY_IS_REQUIRED", "Category is required.");
    public static Notification SUBCATEGORY_IS_REQUIRED = new("SUBCATEGORY_IS_REQUIRED", "Subcategory is required.");
    public static Notification PAYMENT_TOTAL_IS_REQUIRED = new("PAYMENT_TOTAL_IS_REQUIRED", "Payment total is required.");
    public static Notification PAYMENT_TYPE_IS_REQUIRED = new("PAYMENT_TYPE_IS_REQUIRED", "Payment type is required.");
    public static Notification TRANSACTION_DATE_IS_REQUIRED = new("TRANSACTION_DATE_IS_REQUIRED", "Transaction date is required.");
    public static Notification TRANSACTION_DATE_INVALID_YEAR = new("TRANSACTION_DATE_INVALID_YEAR", $"The transaction date year must be between the current year minus {CurrentYear.Min} and the current year plus {CurrentYear.Max}.");
    public static Notification FIRST_DUE_DATE_IS_REQUIRED = new("FIRST_DUE_DATE_IS_REQUIRED", "First due date is required when payment type is credit card.");
    public static Notification CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD = new("CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD", "Credit card object is required when payment is credit card.");
    public static Notification CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD = new("CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD", "Credit card object must be null when payment is not credit card.");
    public static Notification DESCRIPTION_MAX_LENGTH = new("DESCRIPTION_MAX_LENGTH", "Description exceeds maximum length.");
    public static Notification CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES = new("CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES", "Credit card is only allowed when category type is expenses.");
    public static Notification SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY = new("SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY", "Subcategory must be a child of the category.");
    public static Notification TRANSACTION_NOT_FOUND = new("TRANSACTION_NOT_FOUND", "Transaction not found");
    public static Notification TRANSACTION_ID_IS_REQUIRED = new("TRANSACTION_ID_IS_REQUIRED", "Transaction Is required.");
    public static Notification CSV_FILE_IS_REQUIRED = new("CSV_FILE_IS_REQUIRED", "Csv file is required for imorting process");
}
