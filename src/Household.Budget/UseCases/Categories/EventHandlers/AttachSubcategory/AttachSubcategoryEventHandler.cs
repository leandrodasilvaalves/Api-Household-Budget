using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public class AttachSubcategoryEventHandler : IAttachSubcategoryEventHandler
{
    private readonly ICategoryRepository _repository;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public AttachSubcategoryEventHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<SubcategoryWasCreatedEventResponse> HandleAsync(SubcategoryWasCreated notification, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);
        var subcategory = notification.Data;
        var category = await _repository.GetByIdAsync($"{subcategory.Category.Id}",
            subcategory.UserId ?? "", cancellationToken);

        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _repository.UpdateAsync(category, cancellationToken);
            Semaphore.Release();
            return new SubcategoryWasCreatedEventResponse(subcategory);
        }
        Semaphore.Release();
        return new SubcategoryWasCreatedEventResponse(CategoryErrors.CATEGORY_NOT_FOUND);
    }
}
