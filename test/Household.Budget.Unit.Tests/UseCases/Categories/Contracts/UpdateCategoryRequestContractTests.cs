using AutoFixture.Xunit2;

using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

namespace Household.Budget.Unit.Tests.UseCases.Categories.Contracts;

public class UpdateCategoryRequestContractTests
{
    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShouldFailWhenNameIsNull(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithNullName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShouldFailWhenNameIsEmpty(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithEmptyName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShouldFailWhenNameIsTooLong(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithLongName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_MAX_LENGTH.Key);
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShouldFailWhenNameIsTooShort(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithShortName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_MIN_LENGTH.Key);
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShouldFailWhenHaveInvalidOnwerType(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithInvalidOnwerType());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER.Key);
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShoudBeSuccessWhenIsValidWithOwnerSystem(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithValidSystemOnwerType());
        result.Notifications.Should().BeEmpty();
    }

    [Theory]
    [CategoriesAutoSubstituteData]
    public void ShouldBeSuccessWhenIsValidWithOwnerUser(UpdateCategoryRequestFaker request)
    {
        var result = new UpdateCategoryRequestContract(request.WithValidUserOnwerType());
        result.Notifications.Should().BeEmpty();
    }
}