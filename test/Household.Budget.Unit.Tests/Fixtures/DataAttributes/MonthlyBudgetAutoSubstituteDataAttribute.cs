using AutoFixture;
using AutoFixture.Xunit2;

using Household.Budget.Unit.Tests.Fixtures.Customizations;
using Household.Budget.Unit.Tests.Fixtures.Customizations.Data;

namespace Household.Budget.Unit.Tests.Fixtures.DataAttributes;

public class MonthlyBudgetAutoSubstituteDataAttribute : AutoDataAttribute
{
    public MonthlyBudgetAutoSubstituteDataAttribute() : base(() => Factory()) { }

    private static Fixture Factory()
    {
        var fixture = new Fixture();

        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customize(new CategoryDataCustomizations());
        fixture.Customize(new MonthlyBudgetDataCustomizations());
        fixture.Customize(new MonthlyBudgetHandlersCustomizations());

        return fixture;
    }
}
