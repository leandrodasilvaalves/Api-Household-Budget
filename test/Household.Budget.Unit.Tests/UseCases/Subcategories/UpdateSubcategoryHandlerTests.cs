using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.UpdateSubcategory;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Subcategories;

public class UpdateSubcategoryHandlerTests
{
    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldReturnErrorWhenCategoryDoesNotExists(UpdateSubcategoryHandler sut,
                                                                 UpdateSubcategoryRequest request,
                                                                 ISubcategoryData subcategoryData,
                                                                 ICategoryData categoryData)
    {
        categoryData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Category)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(CategoryErrors.CATEGORY_NOT_FOUND);
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.DidNotReceive().UpdateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldReturnErrorWhenSubcategoryDoesNotExists(UpdateSubcategoryHandler sut,
                                                                    UpdateSubcategoryRequest request,
                                                                    ISubcategoryData subcategoryData,
                                                                    ICategoryData categoryData)
    {
        subcategoryData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Subcategory)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.DidNotReceive().UpdateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldUpdateSubcategoryAsync(UpdateSubcategoryHandler sut,
                                                   UpdateSubcategoryRequest request,
                                                   ISubcategoryData subcategoryData,
                                                   ICategoryData categoryData)
    {

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await categoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await subcategoryData.Received().UpdateAsync(Arg.Any<Subcategory>(), Arg.Any<CancellationToken>());
    }
}