using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Unit.Tests.Fixtures;
using Household.Budget.UseCases.Categories.ListCategories;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class ListCategoriesHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldListCategoriesAsync(ListCategoriesHandler sut, ICategoryData data, ListCategoriesRequest request)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data?.Items.Should().HaveCountGreaterThan(0);
        await data.Received().GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}