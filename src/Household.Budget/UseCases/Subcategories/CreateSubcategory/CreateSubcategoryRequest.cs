using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;


using MediatR;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;
public class CreateSubcategoryRequest(string name) : Request, IRequest<CreateSubcategoryResponse>
{
    public string Name { get; } = name;

    public Guid CategoryId { get; private set; }

    public override void Validate()
        => AddNotifications(new CreateSubcategoryRequestContract(this));

    public CreateSubcategoryRequest WithCategoryId(Guid id)
    {
        CategoryId = id;
        return this;
    }

    public Subcategory ToModel(Category category) => new()
    {
        Id = $"{Guid.NewGuid()}",
        Name = Name,
        Category = new(category.Id, category.Name),
        UserId = category.UserId,
        Owner = category.Owner,
        Status = ModelStatus.ACTIVE,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };
}
