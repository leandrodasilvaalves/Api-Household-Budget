namespace Household.Budget.Contracts.Models;

public class BaseModel
{
    public string? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}