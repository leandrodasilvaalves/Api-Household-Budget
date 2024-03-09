using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations.Data;

public class TransactionDataCustomizations : DataCustomizations<Transaction, ITransactionData>
{
    public TransactionDataCustomizations() : base(() => Substitute.For<ITransactionData>()) { }
}
