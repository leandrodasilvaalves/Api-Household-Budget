using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures;
using Household.Budget.UseCases.Categories.CreateCategories;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Categories
{
    public class CreateCategoryHandlerTests
    {
        [Theory]
        [CategoriesAutoSubstituteData]
        public async Task ShouldCreateCategoryAsync(CreateCategoryHandler sut, ICategoryData data, CreateCategoryRequest request)
        {
            var result = await sut.HandleAsync(request, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            await data.Received().CreateAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
        }
    }
}