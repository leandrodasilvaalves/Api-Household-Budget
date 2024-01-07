using Flunt.Validations;

using Household.Budget.Contracts;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionRequestContract : Contract<UpdateTransactionRequest>
{
    public UpdateTransactionRequestContract(UpdateTransactionRequest request)
    {
        Requires()
            .IsNotNullOrWhiteSpace(request.Id,
                TransactionErrors.TRANSACTION_ID_IS_REQUIRED)

            .IsSatified(request,
                x => !string.IsNullOrWhiteSpace(x.Description),
                x => x.Description?.Length <= 50,
                TransactionErrors.DESCRIPTION_MAX_LENGTH)

            .IsSatified(request,
                x => !string.IsNullOrEmpty(x.Category?.Id),
                x => Guid.TryParse(x.Category?.Id, out _),
                TransactionErrors.CATEGORY_IS_REQUIRED)

            .IsSatified(request,
                x => !string.IsNullOrEmpty(x.Category?.Subcategory?.Id),
                x => Guid.TryParse(x.Category?.Subcategory?.Id, out _),
                TransactionErrors.SUBCATEGORY_IS_REQUIRED)

            .IsSatified(request,
                x => x.Payment is { } && x.Payment?.Type == PaymentType.CREDIT_CARD,
                x => x.Payment?.CreditCard is not null,
                 TransactionErrors.CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD)

            .IsSatified(request,
                x => x.Payment is { } && x.Payment?.Type != PaymentType.CREDIT_CARD,
                x => x.Payment?.CreditCard is null,
                TransactionErrors.CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD)

            .IsSatified(request,
                x => x.Payment is { } && x.Payment?.Type == PaymentType.CREDIT_CARD,
                x => x.Payment?.CreditCard?.FirstDueDate is not null,
                TransactionErrors.FIRST_DUE_DATE_IS_REQUIRED)

            .IsSatified(request,
                x => x.TransactionDate is not null,
                x => CurrentYear.IsValid(x.TransactionDate?.Year ?? 0),
                TransactionErrors.TRANSACTION_DATE_INVALID_YEAR);
    }

    public UpdateTransactionRequestContract(UpdateTransactionRequest request,
                                            Transaction transaction,
                                            Category category,
                                            Subcategory subcategory)
    {
        Requires()
            .IsNotNull(transaction, TransactionErrors.TRANSACTION_NOT_FOUND)

            .IsSatified(request,
                x => x.Category is not null,
                x => category is not null,
                CategoryErrors.CATEGORY_NOT_FOUND)

            .IsSatified(request,
                x => x.Category is not null, //IMPORTANTE: se trocar uma categoria, deve ser trocada a subcategoria
                x => subcategory is not null,
                SubcategoryErrors.SUBCATEGORY_NOT_FOUND)

            .IsTrue(category?.Subcategories.Any(s => s.Id == subcategory?.Id),
                TransactionErrors.SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY)
                
            .IsSatified(request,
                x => x.Payment?.Type == PaymentType.CREDIT_CARD,
                x => category?.Type == CategoryType.EXPENSES,
                TransactionErrors.CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES);
    }
}