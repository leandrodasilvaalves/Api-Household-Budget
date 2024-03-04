using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

public class SubcategoryChangeCategoryEventHandler : ISubcategoryChangeCategoryEventHandler
{
    private readonly ICategoryData _Data;

    public SubcategoryChangeCategoryEventHandler(ICategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
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
        var category = await _Data.GetByIdAsync(subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);
        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _Data.UpdateAsync(category, cancellationToken);
        }
    }

    private async Task HandleOldCategory(Subcategory subcategory, string categoryId, CancellationToken cancellationToken)
    {
        var category = await _Data.GetByIdAsync(categoryId ?? "", subcategory.UserId ?? "", cancellationToken);
        if (category is not null)
        {
            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _Data.UpdateAsync(category, cancellationToken);
        }
    }
}
