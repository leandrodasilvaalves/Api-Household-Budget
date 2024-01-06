using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class SubcategoryWasCreated : Event<Subcategory>
{
    public SubcategoryWasCreated(Subcategory data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public override string Name => "SUBCATEGORY_WAS_CREATED";

    public override Subcategory Data { get; }
}
