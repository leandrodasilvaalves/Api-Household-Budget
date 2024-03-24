using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

namespace Household.Budget.Unit.Tests.UseCases.Subcategories.Contracts;

public class UpdateSubcategoryRequestContractTests
{
    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameIsNull(UpdateSubcategoryRequestFaker request)
    {
        var result = new UpdateSubcategoryRequestContract(request.WithNameNull());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameIsEmpty(UpdateSubcategoryRequestFaker request)
    {
        var result = new UpdateSubcategoryRequestContract(request.WithNameEmpty());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameHasMoreCharactersThanAllowed(UpdateSubcategoryRequestFaker request)
    {
        var result = new UpdateSubcategoryRequestContract(request.WithNameCharactersMoreThanAllowed());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_MAX_LENGTH.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryNameHasLessCharactersThanAllowed(UpdateSubcategoryRequestFaker request)
    {
        var result = new UpdateSubcategoryRequestContract(request.WithNameCharactersLessThanAllowed());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_NAME_MIN_LENGTH.Key);
    }

    [Theory]
    [SubcategoriesAutoSubstituteData]
    public void ShouldReturnErrorWhenSubcategoryOwnerIsInvalid(UpdateSubcategoryRequestFaker request)
    {
        var result = new UpdateSubcategoryRequestContract(request.WithInvalidOwnerType());
        result.Notifications.Should().Contain(e => e.Key == SubcategoryErrors.SUBCATEGORY_OWNER_FORBIDDEN_FOR_USER.Key);
    }
}