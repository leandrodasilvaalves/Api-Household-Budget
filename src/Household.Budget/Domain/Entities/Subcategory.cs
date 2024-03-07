using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;
using Household.Budget.Contracts.Entities;
using Household.Budget.Domain.Models;

namespace Household.Budget.Domain.Entities;

public class Subcategory : Entity
{
    public string Name { get; set; }
    public CategoryModel Category { get; set; }

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
