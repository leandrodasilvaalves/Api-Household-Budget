using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class DetachSubcategoryEventHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldNotDetachWhenCategoryDoesNotExistsAsync(DetachSubcategoryEventHandler sut, SubCategoryWasExcluded notification, ICategoryData data)
    {
        data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((Category)null);

        await sut.HandleAsync(notification, CancellationToken.None);

        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.DidNotReceive().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldAttachSubcategoryAsync(DetachSubcategoryEventHandler sut, SubCategoryWasExcluded notification, ICategoryData data)
    {
        await sut.HandleAsync(notification, CancellationToken.None);

        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.Received().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }
}