using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.GetCategoryById;
using Household.Budget.UseCases.Categories.ListCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class CategoryHandlersCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new CreateCategoryHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new UpdateCategoryHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new GetCategoryByIdHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new ListCategoriesHandler(fixture.Create<ICategoryData>()));
    }
}