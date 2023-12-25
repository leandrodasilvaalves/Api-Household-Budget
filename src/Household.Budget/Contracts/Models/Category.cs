using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Models;

public class Category : BaseModel
{
    public string? Name { get; set; }
    public CategoryType Type { get; set; }
}