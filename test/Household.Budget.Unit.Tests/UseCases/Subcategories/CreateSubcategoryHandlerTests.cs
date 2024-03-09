using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Subcategories;

public class CreateSubcategoryHandlerTests
{
    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldReturnErrorWhenCategoryDoesNotExistsAsync(CreateSubcategoryHandler sut,
                                                                      CreateSubcategoryRequest request,
                                                                      ICategoryData categoryData,
                                                                      ISubcategoryData subcategoryData)
    {
        categoryData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Category)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(CategoryErrors.CATEGORY_NOT_FOUND);
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.DidNotReceive().CreateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldCreateSubcategoryAsync(CreateSubcategoryHandler sut,
                                                   CreateSubcategoryRequest request,
                                                   ICategoryData categoryData,
                                                   ISubcategoryData subcategoryData)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().CreateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>());
    }
}