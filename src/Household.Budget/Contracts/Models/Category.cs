using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Models;

public class Category : Model
{
    public Category() => Subcategories = [];

    public string? Name { get; set; }
    public CategoryType Type { get; set; }

    public List<CategoryBranch> Subcategories { get; set; }
}