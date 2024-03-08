using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class CategoryDataCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var categoryData = Substitute.For<ICategoryData>();
        categoryData.CreateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        categoryData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<Category>());

        categoryData.UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        categoryData.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<PagedListResult<Category>>());

        fixture.Register(() => categoryData);
    }
}
