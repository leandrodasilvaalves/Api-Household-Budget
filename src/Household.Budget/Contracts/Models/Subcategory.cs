using System.Text.Json.Serialization;

namespace Household.Budget.Contracts.Models;

public class Subcategory : Model
{
    public string? Name { get; set; }
    public CategoryBranch Category { get; set; }
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