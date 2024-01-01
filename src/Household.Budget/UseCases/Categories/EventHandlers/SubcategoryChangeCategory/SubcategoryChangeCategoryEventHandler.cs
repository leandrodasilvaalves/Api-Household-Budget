using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

public class SubcategoryChangeCategoryEventHandler : ISubcategoryChangeCategoryEventHandler
{
    private readonly ICategoryRepository _repository;

    public SubcategoryChangeCategoryEventHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task Handle(SubcategoryChangedCategory notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;

        var taskNewCategory = HandleNewCategory(subcategory, cancellationToken);
        var taskOlCategory = HandleOldCategory(subcategory, notification.OldCategoryId, cancellationToken);
        Task.WaitAll([taskNewCategory, taskOlCategory], cancellationToken);
        return Task.CompletedTask;
    }

    private async Task HandleNewCategory(Subcategory subcategory, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);
        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _repository.UpdateAsync(category, cancellationToken);
        }
    }

    private async Task HandleOldCategory(Subcategory subcategory, string categoryId, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(categoryId ?? "", subcategory.UserId ?? "", cancellationToken);
        if (category is not null)
        {
            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _repository.UpdateAsync(category, cancellationToken);
        }
    }
}
