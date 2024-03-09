using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Subcategories.ListSubcategories;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Subcategories;

public class ListSubcategoriesHandlerTests
{
    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldListSucategoriesAsync(ListSubcategoriesHandler sut,
                                                  ListSubcategoriesRequest request,
                                                  ISubcategoryData data)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data?.Items.Should().HaveCountGreaterThan(0);
        await data.Received().GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}