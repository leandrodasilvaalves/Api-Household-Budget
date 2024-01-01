using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryRequest : Request
{
    public Guid Id { get; private set; }
    public string Name { get; set; } = "";
    public ModelStatus Status { get; set; }
    public ModelOwner Owner { get; set; }
    public CategoryType Type { get; set; }

    public Category ToModel() => new()
    {
        Id = $"{Id}",
        Name = Name,
        Owner = Owner,
        Status = Status,
        UserId = $"{UserId}",
        Type = Type,
        UpdatedAt = DateTime.UtcNow,
    };

    public override void Validate() =>
        AddNotifications(new UpdateCategoryRequestContract(this));

    public UpdateCategoryRequest WithId(Guid id)
    {
        Id = id;
        return this;
    }
}
