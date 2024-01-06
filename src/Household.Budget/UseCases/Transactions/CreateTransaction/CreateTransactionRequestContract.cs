
using Flunt.Validations;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionRequestContract : Contract<CreateTransactionRequest>
{
    public CreateTransactionRequestContract(CreateTransactionRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Category.Id, TransactionErrors.CATEGORY_IS_REQUIRED)
            .IsNotNullOrEmpty(request.Category.Subcategory?.Id ?? "", TransactionErrors.SUBCATEGORY_IS_REQUIRED)
            .IsGreaterThan(request.Payment.Total, 0, TransactionErrors.PAYMENT_TOTAL_IS_REQUIRED)
            .IsNotNull(request.TransactionDate, TransactionErrors.TRANSACTION_DATE_IS_REQUIRED)
            .IsLowerOrEqualsThan(request.Description, 50, TransactionErrors.DESCRIPTION_MAX_LENGTH)
            .IsNotNull(request.Payment.Type, TransactionErrors.PAYMENT_TOTAL_IS_REQUIRED)
            .IsSatified(request,
                x => x.Payment.Type == PaymentType.CREDIT_CARD,
                x => x.Payment.CreditCard is not null,
                TransactionErrors.CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD)
            .IsSatified(request,
                x => x.Payment.Type != PaymentType.CREDIT_CARD,
                x => x.Payment.CreditCard is null,
                TransactionErrors.CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD)
            .IsSatified(request,
                x => x.Payment.Type == PaymentType.CREDIT_CARD,
                x => x.Payment.CreditCard?.FirstDueDate is not null,
                TransactionErrors.FIRST_DUE_DATE_IS_REQUIRED);
    }

    public CreateTransactionRequestContract(CreateTransactionRequest request,
                                            Category category,
                                            Subcategory subcategory)
    {
        Requires()
            .IsNotNull(category, CategoryErrors.CATEGORY_NOT_FOUND)
            .IsNotNull(subcategory, SubcategoryErrors.SUBCATEGORY_NOT_FOUND)
            .IsTrue(category?.Subcategories.Any(s => s.Id == subcategory?.Id),
                TransactionErrors.SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY)
            .IsSatified(request,
                x => x.Payment.Type == PaymentType.CREDIT_CARD,
                x => category?.Type == CategoryType.EXPENSES,
                TransactionErrors.CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES);
    }
}