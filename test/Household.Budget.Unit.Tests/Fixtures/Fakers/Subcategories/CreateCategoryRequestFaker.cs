using Bogus;

using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.CreateCategories;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;

public class CreateCategoryRequestFaker : CreateCategoryRequest
{
    public CreateCategoryRequestFaker() : base("name", default, default)
    {
        Faker = new Faker();
    }
    public Faker Faker { get; }

    public CreateCategoryRequest WithNullName()
    {
        Name = null;
        return this;
    }

    public CreateCategoryRequest WithEmptyName()
    {
        Name = string.Empty;
        return this;
    }

    public CreateCategoryRequest WithLongName()
    {
        Name = Faker.Random.Words(CreateCategoryRequestContract.MaxLengthName + 1);
        return this;
    }

    public CreateCategoryRequest WithShortName()
    {
        Name = Faker.Random.String(CreateCategoryRequestContract.MinLengthName - 1);
        return this;
    }

    public CreateCategoryRequest WithInvalidOnwerType()
    {
        Owner = ModelOwner.SYSTEM;
        UserClaims = [];
        return this;
    }

    public CreateCategoryRequest WithValidSystemOnwerType()
    {
        Owner = ModelOwner.SYSTEM;
        UserClaims = [IdentityClaims.ADMIN_WRITER];
        return this;
    }

    public CreateCategoryRequest WithValidUserOnwerType()
    {
        Owner = ModelOwner.USER;
        UserClaims = [IdentityClaims.USER_WRITER];
        return this;
    }
}