using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.UpdateCategory;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class UpdateCategoryHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldReturnErrorWhenCategoryDoesNotExistsAsync(UpdateCategoryHandler sut, ICategoryData data, UpdateCategoryRequest request)
    {
        data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Category)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors?.Should().Contain(CategoryErrors.CATEGORY_NOT_FOUND);
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.DidNotReceive().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldUpdateCategoryAsync(UpdateCategoryHandler sut, ICategoryData data, UpdateCategoryRequest request)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.Received().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }
}