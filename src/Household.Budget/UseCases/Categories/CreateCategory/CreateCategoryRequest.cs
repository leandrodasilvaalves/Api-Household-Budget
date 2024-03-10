using Household.Budget.Contracts.Enums;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequest : Request
{
    public CreateCategoryRequest(string name,
                                 ModelOwner owner = default,
                                 CategoryType type = default)
    {
        Name = name;
        Owner = owner;
        Type = type;
    }

    public string Name { get; protected set; }

    public ModelOwner Owner { get; protected set; }

    public CategoryType Type { get; protected set; }

    public override void Validate() =>
        AddNotifications(new CreateCategoryRequestContract(this));
}
