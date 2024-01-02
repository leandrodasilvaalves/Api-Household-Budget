using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;
public class CreateSubcategoryRequest(string name, Guid categoryId) : Request
{
    public string Name { get; } = name;

    public Guid CategoryId { get; } = categoryId;

    public override void Validate()
        => AddNotifications(new CreateSubcategoryRequestContract(this));

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
