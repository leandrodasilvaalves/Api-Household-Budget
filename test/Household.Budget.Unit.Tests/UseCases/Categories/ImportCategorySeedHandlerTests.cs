using FluentAssertions;

using Household.Budget.Unit.Tests.Fixtures;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.ImportCategorySeed;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories;

public class ImportCategorySeedHandlerTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public async Task ShouldImportCategoriesAsync(ImportCategorySeedHandler sut,
                                                  ICreateCategoryHandler createCategoryHandler,
                                                  ImportCategorySeedRequest request)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await createCategoryHandler.Received().HandleAsync(Arg.Any<CreateCategoryRequest>(), Arg.Any<CancellationToken>());
        //TODO: verify masstransit
    }
}