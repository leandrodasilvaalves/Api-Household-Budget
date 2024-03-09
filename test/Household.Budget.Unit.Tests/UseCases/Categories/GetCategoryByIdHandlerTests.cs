using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.GetCategoryById;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class GetCategoryByIdHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldReturnErrorWhenCategoryDoesNotExistsAsync(GetCategoryByIdHandler sut, ICategoryData data, GetCategoryByIdRequest request)
    {
        data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Category)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors?.Should().Contain(CategoryErrors.CATEGORY_NOT_FOUND);
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldGetCategoryByIdAsync(GetCategoryByIdHandler sut, ICategoryData data, GetCategoryByIdRequest request)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}
