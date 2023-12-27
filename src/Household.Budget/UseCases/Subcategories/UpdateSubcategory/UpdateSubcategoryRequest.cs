using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryRequest : Request, IRequest<UpdateSubcategoryResponse>
{
    public Guid Id { get; private set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = "";
    public ModelStatus Status { get; set; }
    public ModelOwner Owner { get; set; }
    public CategoryType Type { get; set; }

    public Subcategory ToModel(Category category) => new()
    {
        Id = $"{Id}",
        Category = new(category.Id, category.Name),
        Name = Name,
        Owner = Owner,
        Status = Status,
        UserId = $"{UserId}",
        UpdatedAt = DateTime.UtcNow,
    };

    public override void Validate() =>
        AddNotifications(new UpdateSubcategoryRequestContract(this));

    public UpdateSubcategoryRequest WithId(Guid id)
    {
        Id = id;
        return this;
    }
}
