using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Models;

public abstract class Model
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public ModelOwner? Owner { get; set; } //TODO: Avaliar se deveria estar aqui ou estar em uma model que realmente precisa definir o owner
    public ModelStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}