using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Transactions.CreateTransaction;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Transactions;

public class CreateTransactionHandlerTests
{
    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShouldReturnErrorWhenRequestIsInvalidAsync(CreateTransactionHandler sut,
                                                                 CreateTransactionRequest request,
                                                                 ITransactionData transactionData,
                                                                 ISubcategoryData subcategoryData,
                                                                 ICategoryData categoryData)
    {
        categoryData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Category)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCountGreaterThanOrEqualTo(1);

        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await transactionData.DidNotReceive().UpdateAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>());

    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShouldCreateTransactionAsync(CreateTransactionHandler sut,
                                                   CreateTransactionRequest request,
                                                   ITransactionData transactionData,
                                                   ISubcategoryData subcategoryData,
                                                   ICategoryData categoryData)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await transactionData.Received().CreateAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>());
    }
}