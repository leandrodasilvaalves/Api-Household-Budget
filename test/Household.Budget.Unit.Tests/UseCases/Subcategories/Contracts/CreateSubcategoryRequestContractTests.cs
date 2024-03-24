using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

namespace Household.Budget.Unit.Tests.UseCases.Subcategories.Contracts;

public class CreateSubcategoryRequestContractTests
{
    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameIsNull(CreateSubcategoryRequestFaker request)
    {
        var result = new CreateSubcategoryRequestContract(request.WithNameNull());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameIsEmpty(CreateSubcategoryRequestFaker request)
    {
        var result = new CreateSubcategoryRequestContract(request.WithNameEmpty());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameHasMoreCharactersThanAllowed(CreateSubcategoryRequestFaker request)
    {
        var result = new CreateSubcategoryRequestContract(request.WithNameCharactersMoreThanAllowed());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_MAX_LENGTH.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameHasLessCharactersThanAllowed(CreateSubcategoryRequestFaker request)
    {
        var result = new CreateSubcategoryRequestContract(request.WithNameCharactersLessThanAllowed());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_MIN_LENGTH.Key);
    }
}