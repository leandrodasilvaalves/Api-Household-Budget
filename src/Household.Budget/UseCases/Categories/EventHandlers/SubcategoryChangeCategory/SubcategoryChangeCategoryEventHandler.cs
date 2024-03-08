using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

public class SubcategoryChangeCategoryEventHandler : ISubcategoryChangeCategoryEventHandler
{
    private readonly ICategoryData _data;

    public SubcategoryChangeCategoryEventHandler(ICategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public Task HandleAsync(SubcategoryChangedCategory notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;

        var taskNewCategory = HandleNewCategory(subcategory, cancellationToken);
        var taskOlCategory = HandleOldCategory(subcategory, notification.OldCategoryId, cancellationToken);
        Task.WaitAll([taskNewCategory, taskOlCategory], cancellationToken);
        return Task.CompletedTask;
    }

    private async Task HandleNewCategory(Subcategory subcategory, CancellationToken cancellationToken)
    {
        var category = await _data.GetByIdAsync(subcategory.Category.Id, subcategory.UserId, cancellationToken);
        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _data.UpdateAsync(category, cancellationToken);
        }
    }

    private async Task HandleOldCategory(Subcategory subcategory, string categoryId, CancellationToken cancellationToken)
    {
        var category = await _data.GetByIdAsync(categoryId, subcategory.UserId, cancellationToken);
        if (category is not null)
        {
            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _data.UpdateAsync(category, cancellationToken);
        }
    }
}
