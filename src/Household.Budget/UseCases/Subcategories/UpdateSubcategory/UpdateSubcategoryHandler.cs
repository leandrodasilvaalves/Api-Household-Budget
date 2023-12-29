using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Models;

using MassTransit;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryHandler : IRequestHandler<UpdateSubcategoryRequest, UpdateSubcategoryResponse>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBus _bus;

    public UpdateSubcategoryHandler(ISubcategoryRepository subcategoryRepository,
                                    ICategoryRepository categoryRepository,
                                    IMediator mediator,
                                    IBus bus)
    {
        _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<UpdateSubcategoryResponse> Handle(UpdateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var categoryTask = _categoryRepository.GetByIdAsync($"{request.CategoryId}", request.UserId, cancellationToken);
        var subcategoryTask = _subcategoryRepository.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        Task.WaitAll([categoryTask, subcategoryTask], cancellationToken);

        var category = categoryTask.Result;
        var oldSubcategory = subcategoryTask.Result;

        if (category is null)
        {
            return new UpdateSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        if (oldSubcategory is null)
        {
            return new UpdateSubcategoryResponse(SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
        }

        var updatedSubcategory = request.ToModel(category);
        await _subcategoryRepository.UpdateAsync(updatedSubcategory, cancellationToken);
        await PublishEventsAsync(updatedSubcategory, oldSubcategory, category, cancellationToken);

        return new UpdateSubcategoryResponse(updatedSubcategory);
    }

    private Task PublishEventsAsync(Subcategory updatedSubcategory, Subcategory oldSubcategory, Category category, CancellationToken cancellationToken)
    {
        if (updatedSubcategory.Status == ModelStatus.EXCLUDED)
        {
            _bus.Publish(new SubCategoryWasExcluded(updatedSubcategory), cancellationToken);
        }
        if (category.Id != oldSubcategory.Category.Id && updatedSubcategory.Status == ModelStatus.ACTIVE)
        {
            var oldCategoryId = oldSubcategory.Category.Id ?? "";
            _bus.Publish(new SubcategoryChangedCategory(updatedSubcategory, oldCategoryId), cancellationToken);
        }
        return Task.CompletedTask;
    }
}