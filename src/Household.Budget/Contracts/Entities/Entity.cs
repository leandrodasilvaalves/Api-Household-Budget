using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Entities;

public abstract class Entity
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public ModelOwner? Owner { get; set; }
    public ModelStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}