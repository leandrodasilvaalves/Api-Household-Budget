using FluentAssertions;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Transactions;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

namespace Household.Budget.Unit.Tests.UseCases.Transactions.Contracts;

public class UpdateTransactionRequestContractTeste
{
    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhen(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithMoreDescriptionThanAllowed());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.DESCRIPTION_MAX_LENGTH.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldNotReturnErrorWhenCategoryIdIsNull(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithCategoryIdNull());
        result.Notifications.Should().NotContain(e => e.Key == TransactionErrors.CATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldNotReturnErrorWhenCategoryIdIsEmpty(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithCategoryIdEmpty());
        result.Notifications.Should().NotContain(e => e.Key == TransactionErrors.CATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryIdIsNotUuid(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithCategoryInvalidId());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldNotReturnErrorWhenSubcategoryIdIsNull(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithSubcategoryIdNull());
        result.Notifications.Should().NotContain(e => e.Key == TransactionErrors.SUBCATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldNotReturnErrorWhenSubcategoryIdIsEmpty(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithSubcategoryIdEmpty());
        result.Notifications.Should().NotContain(e => e.Key == TransactionErrors.SUBCATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryIdIsNotUuid(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithSubcategoryInvalidId());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.SUBCATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentIsCreditCardAndCreditCardObjectIsNull(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithCreditCardObjectNullWhenPaymentTypeIsCreditCard());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentIsNotCreditCardAndCreditCardObjectIsNotNull(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithCreditCardObjectNotNullWhenPaymentTypeIsNotCreditCard());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentIsCreditCardAndFirstDueDateIsNull(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithFirstDueDateNullWhenPaymentTypeIsCreditCard());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.FIRST_DUE_DATE_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenTransactionDateYearIsInvalid(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithInvalidTransactionDateYear());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.TRANSACTION_DATE_INVALID_YEAR.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldNotReturnErrorWhenTransactionDateIsNull(UpdateTransactionRequestFaker request)
    {
        var result = new UpdateTransactionRequestContract(request.WithTransactionDateNull());
        result.Notifications.Should().NotContain(e => e.Key == TransactionErrors.TRANSACTION_DATE_INVALID_YEAR.Key);
        result.Notifications.Should().NotContain(e => e.Key == TransactionErrors.TRANSACTION_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenTransactionDoesNotExists(UpdateTransactionRequestFaker request, Category category,
                                                              Subcategory subcategory)
    {
        var result = new UpdateTransactionRequestContract(request, null, category, subcategory);
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.TRANSACTION_NOT_FOUND.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryDoesNotExists(UpdateTransactionRequestFaker request, Transaction transaction,
                                                              Subcategory subcategory)
    {
        var result = new UpdateTransactionRequestContract(request, transaction, null, subcategory);
        result.Notifications.Should().Contain(e => e.Key == CategoryErrors.CATEGORY_NOT_FOUND.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryDoesNotExists(UpdateTransactionRequestFaker request, Transaction transaction,
                                                              Category category)
    {
        var result = new UpdateTransactionRequestContract(request, transaction, category, null);
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NOT_FOUND.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryIsNotParentOfSubcategory(UpdateTransactionRequestFaker request, Transaction transaction,
                                                              Category category, Subcategory subcategory)
    {
        var result = new UpdateTransactionRequestContract(request, transaction, category, subcategory);
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentTypeIsCreditCardAnbCategoryTypeIsNotExpenses(UpdateTransactionRequestFaker request, Transaction transaction,
                                                              Category category, Subcategory subcategory)
    {
        request.Payment.Type = PaymentType.CREDIT_CARD;
        category.Type = CategoryType.INCOMES;

        var result = new UpdateTransactionRequestContract(request, transaction, category, subcategory);
        
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES.Key);
    }
}