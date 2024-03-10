using AutoFixture.Xunit2;

using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;
using Household.Budget.UseCases.Categories.CreateCategories;

namespace Household.Budget.Unit.Tests.UseCases.Categories.CreateCategory;

public class CreateCategoryRequestContractTests
{
    [Theory]
    [AutoData]
    public void ShouldFailWhenNameIsNull(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithNullName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [AutoData]
    public void ShouldFailWhenNameIsEmpty(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithEmptyName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_IS_REQUIRED.Key);
    }

    [Theory]
    [AutoData]
    public void ShouldFailWhenNameIsTooLong(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithLongName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_MAX_LENGTH.Key);
    }

    [Theory]
    [AutoData]
    public void ShouldFailWhenNameIsTooShort(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithShortName());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NAME_MIN_LENGTH.Key);
    }

    [Theory]
    [AutoData]
    public void ShouldFailWhenHaveInvalidOnwerType(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithInvalidOnwerType());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_OWNER_FORBIDDEN_FOR_USER.Key);
    }

    [Theory]
    [AutoData]
    public void ShoudBeSuccessWhenIsValidWithOwnerSystem(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithValidSystemOnwerType());
        result.Notifications.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void ShouldBeSuccessWhenIsValidWithOwnerUser(CreateCategoryRequestFaker request)
    {
        var result = new CreateCategoryRequestContract(request.WithValidUserOnwerType());
        result.Notifications.Should().BeEmpty();
    }
}