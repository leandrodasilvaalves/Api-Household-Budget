using AutoFixture;
using AutoFixture.Xunit2;

using Household.Budget.Unit.Tests.Fixtures.Customizations;

namespace Household.Budget.Unit.Tests.Fixtures.DataAttributes;

public class CategoriesAutoSubstituteDataAttribute : AutoDataAttribute
{
    public CategoriesAutoSubstituteDataAttribute() : base(() => Factory()) { }

    private static Fixture Factory()
    {
        var fixture = new Fixture();

        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customize(new CategoryDataCustomizations());
        fixture.Customize(new CategoryHandlersCustomizations());

        return fixture;
    }
}
