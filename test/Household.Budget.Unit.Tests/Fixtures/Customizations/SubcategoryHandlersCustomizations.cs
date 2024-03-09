using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Categories.GetSubcategoryById;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;
using Household.Budget.UseCases.Subcategories.ListSubcategories;

using MassTransit;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class SubcategoryHandlersCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => Substitute.For<IBus>());
        fixture.Register(() => new CreateSubcategoryHandler(fixture.Create<ISubcategoryData>(),
                                                            fixture.Create<ICategoryData>(),
                                                            fixture.Create<IBus>()));

        fixture.Register(() => new UpdateSubcategoryHandler(fixture.Create<ISubcategoryData>(),
                                                            fixture.Create<ICategoryData>(),
                                                            fixture.Create<IBus>()));
        
        fixture.Register(() => new GetSubcategoryByIdHandler(fixture.Create<ISubcategoryData>()));
        fixture.Register(() => new ListSubcategoriesHandler(fixture.Create<ISubcategoryData>()));
    }
}