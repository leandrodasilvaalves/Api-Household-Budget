
using Flunt.Validations;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

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
            .IsNotNull(request.Payment.CreditCard?.FirstDueDate, TransactionErrors.TRANSACTION_DATE_IS_REQUIRED)
            .IsRequiredWhen(request.Payment.CreditCard, () => request.Payment.Type == PaymentType.CREDIT_CARD, TransactionErrors.CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD)
            .IsNotAllowedWhen(request.Payment.CreditCard, () => request.Payment.Type != PaymentType.CREDIT_CARD, TransactionErrors.CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD)
            .IsLowerOrEqualsThan(request.Description, 50, TransactionErrors.DESCRIPTION_MAX_LENGTH)
            .IsNotNull(request.Payment.Type, TransactionErrors.PAYMENT_TOTAL_IS_REQUIRED);
    }
}

