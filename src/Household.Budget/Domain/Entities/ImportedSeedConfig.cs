using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Entities;

namespace Household.Budget.Domain.Entities;

public class ImportedSeedConfig : Entity
{
    public bool RootUserHasBeenCreated { get; set; }
    public bool CategoriesHasBeenImported { get; set; }

    public void UpdateRootUserConfig()
    {
        UpdateOtherFields();
        RootUserHasBeenCreated = true;
    }

    public void UpdateCategoryConfig()
    {
        UpdateOtherFields();
        CategoriesHasBeenImported = true;
    }

    private void UpdateOtherFields()
    {
        Id ??= $"{Guid.NewGuid()}";
        if (CreatedAt == default)
        {
            CreatedAt = DateTime.UtcNow;
        }
        UpdatedAt = DateTime.UtcNow;
        Owner ??= ModelOwner.SYSTEM;
        Status = ModelStatus.ACTIVE;
    }
}