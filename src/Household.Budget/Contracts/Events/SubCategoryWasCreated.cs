using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class SubCategoryWasCreated : IEvent<Subcategory>
{
    public SubCategoryWasCreated(Subcategory data)
    {
        Data = data;
        SendedAt = DateTime.UtcNow;
    }

    public string Name => "SUBCATEGORY_WAS_CREATED";

    public Subcategory Data { get; }

    public DateTime SendedAt { get; }
}

public class SubCategoryWasUpdated : IEvent<Subcategory>
{
    public SubCategoryWasUpdated(Subcategory data)
    {
        Data = data;
        SendedAt = DateTime.UtcNow;
    }

    public string Name => "SUBCATEGORY_WAS_UPDATED";

    public Subcategory Data { get; }

    public DateTime SendedAt { get; }
}