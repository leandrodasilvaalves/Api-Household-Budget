using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class SubCategoryWasExcluded : IEvent<Subcategory>
{
    public SubCategoryWasExcluded(Subcategory data)
    {
        Data = data;
        SendedAt = DateTime.UtcNow;
    }

    public string Name => "SUBCATEGORY_WAS_EXCLUDED";

    public Subcategory Data { get; }

    public DateTime SendedAt { get; }
}
