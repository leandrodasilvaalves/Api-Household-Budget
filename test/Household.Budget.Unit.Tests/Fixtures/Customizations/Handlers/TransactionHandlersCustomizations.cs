using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Transactions.CreateTransaction;
using Household.Budget.UseCases.Transactions.GetTransactionById;
using Household.Budget.UseCases.Transactions.ListTransactions;

using MassTransit;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class TransactionHandlersCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => Substitute.For<IBus>());
        fixture.Register(() => new CreateTransactionHandler(fixture.Create<ITransactionData>(),
                                                            fixture.Create<ICategoryData>(),
                                                            fixture.Create<ISubcategoryData>(),
                                                            fixture.Create<IBus>()));

        fixture.Register(() => new GetTransacationByIdHandler(fixture.Create<ITransactionData>()));
        fixture.Register(() => new ListTransactionHandler(fixture.Create<ITransactionData>()));
    }
}