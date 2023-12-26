namespace Household.Budget.Contracts.Models;

public class Subcategory : Model
{
    public string? Name { get; set; }
    public CategoryBranch Category { get; set; }
}

public readonly struct CategoryBranch(string? id, string? name)
{
    public string? Id { get; } = id;
    public string? Name { get; } = name;
}