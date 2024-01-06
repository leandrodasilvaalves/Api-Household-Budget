using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class SubcategoryChangedCategory : Event<Subcategory>
{
    public SubcategoryChangedCategory(Subcategory data, string oldCategoryId)
    {
        if (string.IsNullOrEmpty(oldCategoryId))
        {
            throw new ArgumentException($"'{nameof(oldCategoryId)}' cannot be null or empty.", nameof(oldCategoryId));
        }

        Data = data ?? throw new ArgumentNullException(nameof(data));
        OldCategoryId = oldCategoryId;
    }

    public override string Name => "SUBCATEGORY_CHANGED_CATEGORY";

    public override Subcategory Data { get; }

    public string OldCategoryId { get; }
}