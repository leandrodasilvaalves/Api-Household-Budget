using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;

public class DetachSubcategoryEventHandler : IDetachSubcategoryEventHandler
{
    private readonly ICategoryData _data;

    public DetachSubcategoryEventHandler(ICategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task HandleAsync(SubcategoryWasExcluded notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;
        var category = await _data.GetByIdAsync(subcategory.Category.Id, subcategory.UserId, cancellationToken);
        if (category is not null)
        {
            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _data.UpdateAsync(category, cancellationToken);
        }
    }
}