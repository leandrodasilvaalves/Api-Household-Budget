using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class SubcategoryChangeCategoryEventHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldNotUpdateWhenCategoriesDoesNotExistsAsync(SubcategoryChangeCategoryEventHandler sut,
                                                                      ICategoryData data,
                                                                      SubcategoryChangedCategory notification)
    {
        data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Category)null);
        await sut.HandleAsync(notification, CancellationToken.None);

        await data.Received(2).GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.DidNotReceive().UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }

    
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldUpdateWhenCategoriesDoesExistsAsync(SubcategoryChangeCategoryEventHandler sut,
                                                                      ICategoryData data,
                                                                      SubcategoryChangedCategory notification)
    {
        await sut.HandleAsync(notification, CancellationToken.None);

        await data.Received(2).GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await data.Received(2).UpdateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }
}