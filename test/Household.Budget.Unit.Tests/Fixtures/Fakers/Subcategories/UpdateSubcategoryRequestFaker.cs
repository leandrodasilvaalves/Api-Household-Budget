using Bogus;

using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.UpdateSubcategory;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;

public class UpdateSubcategoryRequestFaker : UpdateSubcategoryRequest
{
    public UpdateSubcategoryRequestFaker()
        => Faker = new Faker();

    private Faker Faker { get; }

    public UpdateSubcategoryRequest WithNameNull()
    {
        Name = null;
        return this;
    }

    public UpdateSubcategoryRequest WithNameEmpty()
    {
        Name = string.Empty;
        return this;
    }

    public UpdateSubcategoryRequest WithNameCharactersMoreThanAllowed()
    {
        Name = Faker.Random.Words(UpdateSubcategoryRequestContract.NAME_MAX_LENGTH + 1);
        return this;
    }

    public UpdateSubcategoryRequest WithNameCharactersLessThanAllowed()
    {
        Name = Faker.Random.String(UpdateSubcategoryRequestContract.NAME_MIN_LENGTH - 1);
        return this;
    }

    public UpdateSubcategoryRequest WithInvalidOwnerType()
    {
        Owner = ModelOwner.SYSTEM;
        UserClaims = [];
        return this;
    }
}