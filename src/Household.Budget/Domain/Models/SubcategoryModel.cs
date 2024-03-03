using System.Text.Json.Serialization;

namespace Household.Budget.Domain.Models;

public readonly struct SubcategoryModel
{
    [JsonConstructor]
    public SubcategoryModel(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; }
}