using FluentAssertions;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Transactions;
using Household.Budget.UseCases.Transactions.CreateTransaction;

namespace Household.Budget.Unit.Tests.UseCases.Transactions.Contracts;

public class CreateTransactionRequestContractTests
{
    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryIdIsNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithCategoryIdNull());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryIdIsEmpty(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithCategoryIdEmpty());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryIdIsNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithSubcategoryIdNull());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.SUBCATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryIdIsEmpty(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithSubcategoryIdEmpty());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.SUBCATEGORY_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenWhenTransactionDateIsNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithTransactionDateNull());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.TRANSACTION_DATE_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenWhenDescriptionHasMoreCharactersThanAllowed(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithMoreDescriptionThanAllowed());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.DESCRIPTION_MAX_LENGTH.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenWhenPaymentTypeIsNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithPaymentTypeNull());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.PAYMENT_TYPE_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenTransactionDateYearIsInvalid(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithInvalidTransactionDateYear());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.TRANSACTION_DATE_INVALID_YEAR.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentIsCreditCardAndCreditCardObjectIsNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithCreditCardObjectNullWhenPaymentTypeIsCreditCard());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CREDIT_CARD_OBJECT_IS_REQUIRED_WHEN_PAYMENT_IS_CREDIT_CARD.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentIsNotCreditCardAndCreditCardObjectIsNotNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithCreditCardObjectNotNullWhenPaymentTypeIsNotCreditCard());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CREDIT_CARD_OBJECT_MUST_BE_NULL_WHEN_PAYMENT_IS_NOT_CREDIT_CARD.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentIsCreditCardAndFirstDueDateIsNull(CreateTransactionRequestFaker request)
    {
        var result = new CreateTransactionRequestContract(request.WithFirstDueDateNullWhenPaymentTypeIsCreditCard());
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.FIRST_DUE_DATE_IS_REQUIRED.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryDoesNotExists(CreateTransactionRequestFaker request, Subcategory subcategory)
    {
        var result = new CreateTransactionRequestContract(request, null, subcategory);
        result.Notifications.Should().Contain(e => e.Key == CategoryErrors.CATEGORY_NOT_FOUND.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryDoesNotExists(CreateTransactionRequestFaker request, Category category)
    {
        var result = new CreateTransactionRequestContract(request, category, null);
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NOT_FOUND.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenCategoryIsNotParentOfSubcategory(CreateTransactionRequestFaker request, Category category, Subcategory subcategory)
    {
        var result = new CreateTransactionRequestContract(request, category, subcategory);
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY.Key);
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public void ShouldReturnErrorWhenPaymentTypeIsCreditCardAndCategoryTypeIsNotExpenses(CreateTransactionRequestFaker request, Category category)
    {
        category.Type = CategoryType.INCOMES;
        request.Payment.Type = PaymentType.CREDIT_CARD;

        var result = new CreateTransactionRequestContract(request, category, default);
        result.Notifications.Should().Contain(e => e.Key == TransactionErrors.CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES.Key);
    }
}