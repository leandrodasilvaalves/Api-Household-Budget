using AutoFixture;
using AutoFixture.Xunit2;

using Household.Budget.Unit.Tests.Fixtures.Customizations;

namespace Household.Budget.Unit.Tests.Fixtures
{
    public class CategoriesAutoSubstituteDataAttribute : AutoDataAttribute
    {
        public CategoriesAutoSubstituteDataAttribute() : base(() => Factory()) { }

        private static Fixture Factory()
        {
            var fixture = new Fixture();
            fixture.Customize(new CategoryDataCustomizations());
            fixture.Customize(new CreateCategoryHandlerCustomizations());
            
            return fixture;
        }
    }
}