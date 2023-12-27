using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class SubcategoryChangedCategory : IEvent<Subcategory>
{
    public SubcategoryChangedCategory(Subcategory data, string oldCategoryId)
    {
        Data = data;
        SendedAt = DateTime.UtcNow;
        OldCategoryId = oldCategoryId;
    }

    public string Name => "SUBCATEGORY_CHANGED_CATEGORY";

    public Subcategory Data { get; }

    public DateTime SendedAt { get; }

    public string OldCategoryId { get; }
}