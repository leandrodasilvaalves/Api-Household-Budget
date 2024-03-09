using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Entities;

using MassTransit;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryHandler : IUpdateSubcategoryHandler
{
    private readonly ISubcategoryData _subcategoryData;
    private readonly ICategoryData _categoryData;
    private readonly IBus _bus;

    public UpdateSubcategoryHandler(ISubcategoryData subcategoryData,
                                    ICategoryData categoryData,
                                    IBus bus)
    {
        _subcategoryData = subcategoryData ?? throw new ArgumentNullException(nameof(subcategoryData));
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<UpdateSubcategoryResponse> HandleAsync(UpdateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var categoryTask = _categoryData.GetByIdAsync(request.CategoryId, request.UserId, cancellationToken);
        var subcategoryTask = _subcategoryData.GetByIdAsync(request.Id, request.UserId, cancellationToken);
        Task.WaitAll([categoryTask, subcategoryTask], cancellationToken);

        var category = categoryTask.Result;
        var subcategory = subcategoryTask.Result;

        if (category is null)
        {
            return new UpdateSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        if (subcategory is null)
        {
            return new UpdateSubcategoryResponse(SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
        }

        var oldSubcategory = subcategory.Clone();
        subcategory.Update(request, category);
        await _subcategoryData.UpdateAsync(subcategory, cancellationToken);
        await PublishEventsAsync(subcategory, oldSubcategory, category, cancellationToken);

        return new UpdateSubcategoryResponse(subcategory);
    }

    private Task PublishEventsAsync(Subcategory updatedSubcategory, Subcategory oldSubcategory, Category category, CancellationToken cancellationToken)
    {
        if (updatedSubcategory.Status == ModelStatus.EXCLUDED)
        {
            _bus.Publish(new SubcategoryWasExcluded(updatedSubcategory), cancellationToken);
        }
        if (category.Id != oldSubcategory.Category.Id && updatedSubcategory.Status == ModelStatus.ACTIVE)
        {
            var oldCategoryId = oldSubcategory.Category.Id ?? "";
            _bus.Publish(new SubcategoryChangedCategory(updatedSubcategory, oldCategoryId), cancellationToken);
        }
        return Task.CompletedTask;
    }
}