using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations.Data;

public class TransactionDataCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var transactionData = Substitute.For<ITransactionData>();
        transactionData.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(),
                                    Arg.Any<CancellationToken>())
                        .Returns(fixture.Create<PagedListResult<Transaction>>());

        fixture.Register(() => transactionData);
        var x = new DataCustomizations<Transaction, ITransactionData>(() => fixture.Create<ITransactionData>());
        x.Customize(fixture);
    }
}
