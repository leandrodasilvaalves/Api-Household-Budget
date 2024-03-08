using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public class AttachSubcategoryEventHandler : IAttachSubcategoryEventHandler
{
    private readonly ICategoryData _data;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public AttachSubcategoryEventHandler(ICategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<AttachSubcategoryResponse> HandleAsync(SubcategoryWasCreated notification, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);
        var subcategory = notification.Data;
        var category = await _data.GetByIdAsync($"{subcategory.Category.Id}",
            subcategory.UserId ?? "", cancellationToken);

        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _data.UpdateAsync(category, cancellationToken);
            Semaphore.Release();
            return new AttachSubcategoryResponse(subcategory);
        }
        Semaphore.Release();
        return new AttachSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
    }
}
