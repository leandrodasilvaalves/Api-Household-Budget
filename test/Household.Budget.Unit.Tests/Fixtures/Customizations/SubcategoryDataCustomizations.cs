using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class SubcategoryDataCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var subcategoryData = Substitute.For<ISubcategoryData>();
        subcategoryData.CreateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        subcategoryData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<Subcategory>());

        subcategoryData.UpdateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        subcategoryData.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<PagedListResult<Subcategory>>());

        fixture.Register(() => subcategoryData);
    }
}
