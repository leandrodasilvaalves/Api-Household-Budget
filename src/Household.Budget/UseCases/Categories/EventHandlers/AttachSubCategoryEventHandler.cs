using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;

using MediatR;

namespace Household.Budget.UseCases.Categories.EventHandlers;

public class AttachSubCategoryEventHandler : INotificationHandler<SubcategoryWasCreated>
{
    private readonly ICategoryRepository _repository;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public AttachSubCategoryEventHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(SubcategoryWasCreated notification, CancellationToken cancellationToken)
    {
        await Semaphore.WaitAsync(cancellationToken);

        var subcategory = notification.Data;
        var category = await _repository.GetByIdAsync($"{subcategory.Category.Id}",
            subcategory.UserId ?? "", cancellationToken);

        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _repository.UpdateAsync(category, cancellationToken);
        }
        
        Semaphore.Release();
    }
}