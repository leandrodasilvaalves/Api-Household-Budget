using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class SubcategoryWasCreated : IEvent<Subcategory>
{
    public SubcategoryWasCreated(Subcategory data)
    {
        Data = data;
        SendedAt = DateTime.UtcNow;
    }

    public string Name => "SUBCATEGORY_WAS_CREATED";

    public Subcategory Data { get; }

    public DateTime SendedAt { get; }
}
