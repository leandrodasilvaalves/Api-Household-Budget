using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.UseCases.Categories.CreateCategories;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations
{
    public class CreateCategoryHandlerCustomizations : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var categoryData = Substitute.For<ICategoryData>();
            categoryData.CreateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask);

            fixture.Register(() => categoryData);
            fixture.Register(() => new CreateCategoryHandler(fixture.Create<ICategoryData>()));
        }
    }
}