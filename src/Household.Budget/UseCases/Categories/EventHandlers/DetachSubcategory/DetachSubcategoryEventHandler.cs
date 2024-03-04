using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;

public class DetachSubcategoryEventHandler : IDetachSubcategoryEventHandler
{
    private readonly ICategoryData _Data;

    public DetachSubcategoryEventHandler(ICategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task HandleAsync(SubCategoryWasExcluded notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;
        var category = await _Data.GetByIdAsync(
            subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);
        if (category is not null)
        {
            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _Data.UpdateAsync(category, cancellationToken);
        }
    }
}