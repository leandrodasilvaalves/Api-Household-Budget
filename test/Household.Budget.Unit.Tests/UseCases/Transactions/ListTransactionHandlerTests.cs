using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Transactions.ListTransactions;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Transactions;

public class ListTransactionHandlerTests
{
    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShouldListTransactionsAsync(ListTransactionHandler sut,
                                                  ListTransactionRequest request,
                                                  ITransactionData data)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data?.Items.Should().HaveCountGreaterThan(0);
        await data.Received().GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(),
                                          Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}