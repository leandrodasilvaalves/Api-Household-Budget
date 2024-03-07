using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class CreateCategoryHandlerCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new CreateCategoryHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new UpdateCategoryHandler(fixture.Create<ICategoryData>()));
    }
}