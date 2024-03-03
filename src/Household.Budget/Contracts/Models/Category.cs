using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

namespace Household.Budget.Contracts.Models;

public class Category : Model
{
    public Category() => Subcategories = [];

    public string? Name { get; set; }
    public CategoryType Type { get; set; }

    public List<CategoryBranch> Subcategories { get; set; }

    public void Create(CreateCategoryRequest request)
    {
        Id = $"{Guid.NewGuid()}";
        Name = request.Name;
        Owner = request.Owner;
        Status = ModelStatus.ACTIVE;
        Type = request.Type;
        UserId = request.UserId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(UpdateCategoryRequest request)
    {
        Name = request.Name;
        Owner = request.Owner;
        Status = request.Status;
        Type = request.Type;
        UpdatedAt = DateTime.UtcNow;
    }
}