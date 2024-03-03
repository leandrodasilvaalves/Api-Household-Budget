using System.Text.Json.Serialization;

using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

namespace Household.Budget.Contracts.Models;

public class Subcategory : Model
{
    public string? Name { get; set; }
    public CategoryBranch Category { get; set; }

    public Subcategory Clone() => (Subcategory)this.MemberwiseClone();

    public void Create(CreateSubcategoryRequest request, Category category)
    {
        Id = $"{Guid.NewGuid()}";
        Name = request.Name;
        Category = new(category.Id, category.Name);
        UserId = category.UserId;
        Owner = category.Owner;
        Status = ModelStatus.ACTIVE;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Update(UpdateSubcategoryRequest request, Category category)
    {
        Id = request.Id;
        Category = new(category.Id, category.Name);
        Name = request.Name;
        Owner = request.Owner;
        Status = request.Status;
        UserId = request.UserId;
        UpdatedAt = DateTime.UtcNow;
    }
}

public readonly struct CategoryBranch
{
    [JsonConstructor]
    public CategoryBranch(string? id, string? name)
    {
        Id = id;
        Name = name;
    }

    public string? Id { get; }
    public string? Name { get; }
}