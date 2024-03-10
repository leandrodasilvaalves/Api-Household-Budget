using Bogus;

using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Entities;
using Household.Budget.UseCases.Categories.UpdateCategory;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;

public class UpdateCategoryRequestFaker : UpdateCategoryRequest
{
    public UpdateCategoryRequestFaker()
    {
        Faker = new Faker();
        Name = "name";
        Type = default;
        Owner = default;
        Status = default;
    }

    public Faker Faker { get; }

    public UpdateCategoryRequest WithNullName()
    {
        Name = null;
        return this;
    }

    public UpdateCategoryRequest WithEmptyName()
    {
        Name = string.Empty;
        return this;
    }

    public UpdateCategoryRequest WithLongName()
    {
        Name = Faker.Random.Words(Category.MaxLengthName + 1);
        return this;
    }

    public UpdateCategoryRequest WithShortName()
    {
        Name = Faker.Random.String(Category.MinLengthName - 1);
        return this;
    }

    public UpdateCategoryRequest WithInvalidOnwerType()
    {
        Owner = ModelOwner.SYSTEM;
        UserClaims = [];
        return this;
    }

    public UpdateCategoryRequest WithValidSystemOnwerType()
    {
        Owner = ModelOwner.SYSTEM;
        UserClaims = [IdentityClaims.ADMIN_WRITER];
        return this;
    }

    public UpdateCategoryRequest WithValidUserOnwerType()
    {
        Owner = ModelOwner.USER;
        UserClaims = [IdentityClaims.USER_WRITER];
        return this;
    }
}