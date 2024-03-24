using Bogus;

using Household.Budget.UseCases.Subcategories.CreateSubcategory;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.Subcategories;

public class CreateSubcategoryRequestFaker : CreateSubcategoryRequest
{
    public CreateSubcategoryRequestFaker() : base("name", "categoryId")
        => Faker = new Faker();

    private Faker Faker { get; }

    public CreateSubcategoryRequest WithNameNull()
    {
        Name = null;
        return this;
    }

    public CreateSubcategoryRequest WithNameEmpty()
    {
        Name = string.Empty;
        return this;
    }

    public CreateSubcategoryRequest WithNameCharactersMoreThanAllowed()
    {
        Name = Faker.Random.Words(CreateSubcategoryRequestContract.NAME_MAX_LENGTH + 1);
        return this;
    }

    public CreateSubcategoryRequest WithNameCharactersLessThanAllowed()
    {
        Name = Faker.Random.String(CreateSubcategoryRequestContract.NAME_MIN_LENGTH - 1);
        return this;
    }
}