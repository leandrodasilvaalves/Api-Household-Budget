using AutoFixture;

using Bogus;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Helpers;
using Household.Budget.Domain.Models;
using Household.Budget.UseCases.Transactions.CreateTransaction;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.Transactions;

public class CreateTransactionRequestFaker : CreateTransactionRequest
{
    private readonly IFixture _fixture;

    public CreateTransactionRequestFaker(IFixture fixture)
    {
        Description = $"Descritpion_{Guid.NewGuid()}";
        Category = fixture.Create<CategoryModel>();
        Category.Subcategory = fixture.Create<CategoryModel>();
        Payment = fixture.Create<PaymentModel>();
        TransactionDate = DateTime.Now;
        Tags = [.. $"{Guid.NewGuid()}".Split("-")];
        _fixture = fixture;
    }

    public CreateTransactionRequest WithCategoryIdNull()
    {
        Category.Id = null;
        return this;
    }
    public CreateTransactionRequest WithCategoryIdEmpty()
    {

        Category.Id = "";
        return this;
    }

    public CreateTransactionRequest WithSubcategoryIdNull()
    {
        Category.Subcategory.Id = null;
        return this;
    }
    public CreateTransactionRequest WithSubcategoryIdEmpty()
    {
        Category.Subcategory.Id = "";
        return this;
    }

    public CreateTransactionRequest WithMoreDescriptionThanAllowed()
    {
        Description = new Faker()
            .Random.Words(CreateTransactionRequestContract.DESCRIPTION_MAX_LENGTH + 1);
        return this;
    }

    public CreateTransactionRequest WithPaymentTotalEqualsToZero()
    {
        Payment.Total = 0;
        return this;
    }

    public CreateTransactionRequest WithPaymentTotalLessThanZero()
    {

        Payment.Total = -1;
        return this;
    }

    public CreateTransactionRequest WithTransactionDateNull()
    {
        TransactionDate = null;
        return this;
    }

    public CreateTransactionRequest WithInvalidTransactionDateYear()
    {
        TransactionDate = DateTime.Now.AddYears(Year.Max + 1);
        return this;
    }

    public CreateTransactionRequest WithCreditCardObjectNullWhenPaymentTypeIsCreditCard()
    {
        Payment.Type = PaymentType.CREDIT_CARD;
        Payment.CreditCard = null;
        return this;
    }

    public CreateTransactionRequest WithCreditCardObjectNotNullWhenPaymentTypeIsNotCreditCard()
    {
        Payment.Type = PaymentType.PIX;
        Payment.CreditCard = _fixture.Create<CreditCardModel>();
        return this;
    }

    public CreateTransactionRequest WithFirstDueDateNullWhenPaymentTypeIsCreditCard()
    {
        Payment.Type = PaymentType.CREDIT_CARD;
        Payment.CreditCard = _fixture.Create<CreditCardModel>();
        Payment.CreditCard.FirstDueDate = null;
        return this;
    }

    public CreateTransactionRequest WithPaymentTypeNull()
    {
        Payment.Type = null;
        return this;
    }
}