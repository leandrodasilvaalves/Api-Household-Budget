using Household.Budget.Contracts.Enums;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryRequest : Request
{
    public string Id { get; private set; }
    public string Name { get; set; } = "";
    public ModelStatus Status { get; set; }
    public ModelOwner Owner { get; set; }
    public CategoryType Type { get; set; }

    public override void Validate() =>
        AddNotifications(new UpdateCategoryRequestContract(this));

    public UpdateCategoryRequest WithId(string id)
    {
        Id = id;
        return this;
    }
}
