using Household.Budget.Contracts.Enums;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryRequest : Request
{
    public string? Id { get; private set; }
    public string? CategoryId { get; set; }
    public string Name { get; set; } = "";
    public ModelStatus Status { get; set; }
    public ModelOwner Owner { get; set; }
    public CategoryType Type { get; set; }

    public override void Validate() =>
        AddNotifications(new UpdateSubcategoryRequestContract(this));

    public UpdateSubcategoryRequest WithId(string id)
    {
        Id = id;
        return this;
    }
}
