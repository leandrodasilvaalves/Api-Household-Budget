using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Models;

public class Category : BaseModel
{
    public ModelType Type { get; set; }
    public string? Name { get; set; }
    public ModelStatus Status { get; set; }
    public Guid UserId { get; set; }
}