using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryHandler : IRequestHandler<UpdateSubcategoryRequest, UpdateSubcategoryResponse>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMediator _mediator;

    public UpdateSubcategoryHandler(ISubcategoryRepository subcategoryRepository,
                                    ICategoryRepository categoryRepository,
                                    IMediator mediator)
    {
        _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<UpdateSubcategoryResponse> Handle(UpdateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var category = _categoryRepository.GetByIdAsync($"{request.CategoryId}", request.UserId, cancellationToken);
        var subcategory = _subcategoryRepository.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        Task.WaitAll([category, subcategory], cancellationToken);

        if (category.Result is null)
        {
            return new UpdateSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        if (subcategory.Result is null)
        {
            return new UpdateSubcategoryResponse(SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
        }

        var updatedSubcategory = request.ToModel(category.Result);
        await _subcategoryRepository.UpdateAsync(updatedSubcategory, cancellationToken);
        if (updatedSubcategory.Status == ModelStatus.EXCLUDED)
        {
            await _mediator.Publish(new SubCategoryWasExcluded(updatedSubcategory), cancellationToken);
        }
        if (category.Result.Id != subcategory.Result.Category.Id && updatedSubcategory.Status == ModelStatus.ACTIVE)
        {
            var oldCategoryId = subcategory.Result.Category.Id ?? "";
            await _mediator.Publish(new SubcategoryChangedCategory(updatedSubcategory, oldCategoryId), cancellationToken);
        }

        return new UpdateSubcategoryResponse(updatedSubcategory);
    }
}