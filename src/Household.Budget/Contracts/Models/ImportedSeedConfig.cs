using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.Models;

public class ImportedSeedConfig : Model
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