using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.Categories.GetSubcategoryById;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.Subcategories;

public class GetSubcategoryByIdHandlerTests
{
    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShoulReturnErrorWhenSubcategoryDoesNotExistsAsync(GetSubcategoryByIdHandler sut,
                                                                        GetSubcategoryByIdRequest request,
                                                                        ISubcategoryData data)
    {
        data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Subcategory)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public async Task ShouldGetSubcategoryByIdAsync(GetSubcategoryByIdHandler sut,
                                                                        GetSubcategoryByIdRequest request,
                                                                        ISubcategoryData data)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await data.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}