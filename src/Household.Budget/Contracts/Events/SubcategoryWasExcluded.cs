using Household.Budget.Domain.Entities;

namespace Household.Budget.Contracts.Events;

public class SubcategoryWasExcluded : Event<Subcategory>
{
    public SubcategoryWasExcluded(Subcategory data) => 
        Data = data ?? throw new ArgumentNullException(nameof(data));

    public override string Name => "SUBCATEGORY_WAS_EXCLUDED";

    public override Subcategory Data { get; }

}