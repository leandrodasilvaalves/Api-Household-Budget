using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

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
    }
}