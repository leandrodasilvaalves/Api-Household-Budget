using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public class AttachSubcategoryEventHandler : IAttachSubcategoryEventHandler
{
    private readonly ICategoryData _Data;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public AttachSubcategoryEventHandler(ICategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<SubcategoryWasCreatedEventResponse> HandleAsync(SubcategoryWasCreated notification, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);
        var subcategory = notification.Data;
        var category = await _Data.GetByIdAsync($"{subcategory.Category.Id}",
            subcategory.UserId ?? "", cancellationToken);

        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _Data.UpdateAsync(category, cancellationToken);
            Semaphore.Release();
            return new SubcategoryWasCreatedEventResponse(subcategory);
        }
        Semaphore.Release();
        return new SubcategoryWasCreatedEventResponse(CategoryErrors.CATEGORY_NOT_FOUND);
    }
}
