using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Transactions;

public class UpdateTransactionHandlerTests
{
    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShoulReturnErrorWhenrRequestIsInvalidAsync(UpdateTransactionHandler sut,
                                                                 UpdateTransactionRequest request,
                                                                 ITransactionData transactionData,
                                                                 ICategoryData categoryData,
                                                                 ISubcategoryData subcategoryData)
    {
        transactionData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((Transaction)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await transactionData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await transactionData.DidNotReceive().UpdateAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShouldUpdateTransactionAsync(UpdateTransactionHandler sut,
                                                   UpdateTransactionRequest request,
                                                   ITransactionData transactionData,
                                                   ICategoryData categoryData,
                                                   ISubcategoryData subcategoryData)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await transactionData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await transactionData.Received().UpdateAsync(Arg.Any<Transaction>(), Arg.Any<CancellationToken>());
    }
}