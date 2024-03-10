using AutoFixture;

using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations.Data;

public class MonthlyBudgetDataCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var monthlyBudgetData = Substitute.For<IMonthlyBudgetData>();
        monthlyBudgetData.ExistsAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns(false);

        monthlyBudgetData.GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<MonthlyBudget>());

        fixture.Register(() => monthlyBudgetData);
        var dataCustomizations = new DataCustomizations<MonthlyBudget, IMonthlyBudgetData>(() => fixture.Create<IMonthlyBudgetData>());
        dataCustomizations.Customize(fixture);
    }
}