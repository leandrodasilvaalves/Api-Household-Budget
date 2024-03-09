using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Transactions.GetTransactionById;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Transactions;

public class GetTransacationByIdHandlerTests
{
    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShouldReturnErrorWhenTransactionDoesNotExistsAsync(GetTransacationByIdHandler sut,
                                                                         GetTransacationByIdRequest request,
                                                                         ITransactionData transactionData)
    {
        transactionData
            .GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((Transaction)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(TransactionErrors.TRANSACTION_NOT_FOUND);
        await transactionData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
    
    [Theory]
    [TransactionsAutoSubstituteData]
    public async Task ShouldGetTransactionByIdAsync(GetTransacationByIdHandler sut,
                                                    GetTransacationByIdRequest request,
                                                    ITransactionData transactionData)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await transactionData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}