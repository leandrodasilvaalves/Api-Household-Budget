using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class AttachSubcategoryEventHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldReturnErrorWhenCategoryDoesNotExistsAsync(AttachSubcategoryEventHandler sut, SubcategoryWasCreated notification, ICategoryData data)
    {
        data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((Category)null);

        var result = await sut.HandleAsync(notification, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(CategoryErrors.CATEGORY_NOT_FOUND);
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.DidNotReceive().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldAttachSubcategoryAsync(AttachSubcategoryEventHandler sut, SubcategoryWasCreated notification, ICategoryData data)
    {
        var result = await sut.HandleAsync(notification, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.Received().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }
}