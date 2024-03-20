using AutoFixture;

using Bogus;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Helpers;
using Household.Budget.Domain.Models;
using Household.Budget.UseCases.Transactions.CreateTransaction;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.Transactions;

public class UpdateTransactionRequestFaker : UpdateTransactionRequest
{
    private readonly IFixture _fixture;

    public UpdateTransactionRequestFaker(IFixture fixture)
    {
        Description = $"Descritpion_{Guid.NewGuid()}";
        Category = fixture.Create<CategoryModel>();
        Category.Subcategory = fixture.Create<CategoryModel>();
        Payment = fixture.Create<PaymentModel>();
        TransactionDate = DateTime.Now;
        Tags = [.. $"{Guid.NewGuid()}".Split("-")];
        _fixture = fixture;
    }

    public UpdateTransactionRequest WithMoreDescriptionThanAllowed()
    {
        Description = new Faker()
            .Random.Words(CreateTransactionRequestContract.DESCRIPTION_MAX_LENGTH + 1);
        return this;
    }

    public UpdateTransactionRequest WithCategoryIdNull()
    {
        Category.Id = null;
        return this;
    }
    public UpdateTransactionRequest WithCategoryIdEmpty()
    {

        Category.Id = "";
        return this;
    }

    public UpdateTransactionRequest WithCategoryInvalidId()
    {
        Category.Id = "category";
        return this;
    }

    public UpdateTransactionRequest WithSubcategoryIdNull()
    {
        Category.Subcategory.Id = null;
        return this;
    }
    public UpdateTransactionRequest WithSubcategoryIdEmpty()
    {
        Category.Subcategory.Id = "";
        return this;
    }

    public UpdateTransactionRequest WithSubcategoryInvalidId()
    {
        Category.Subcategory.Id = "subcategory";
        return this;
    }

    public UpdateTransactionRequest WithCreditCardObjectNullWhenPaymentTypeIsCreditCard()
    {
        Payment.Type = PaymentType.CREDIT_CARD;
        Payment.CreditCard = null;
        return this;
    }

    public UpdateTransactionRequest WithCreditCardObjectNotNullWhenPaymentTypeIsNotCreditCard()
    {
        Payment.Type = PaymentType.PIX;
        Payment.CreditCard = _fixture.Create<CreditCardModel>();
        return this;
    }

    public UpdateTransactionRequest WithFirstDueDateNullWhenPaymentTypeIsCreditCard()
    {
        Payment.Type = PaymentType.CREDIT_CARD;
        Payment.CreditCard = _fixture.Create<CreditCardModel>();
        Payment.CreditCard.FirstDueDate = null;
        return this;
    }

    public UpdateTransactionRequest WithInvalidTransactionDateYear()
    {
        TransactionDate = DateTime.Now.AddYears(Year.Max + 1);
        return this;
    }

    public UpdateTransactionRequest WithTransactionDateNull()
    {
        TransactionDate = null;
        return this;
    }
}
