using Household.Budget.Contracts.Data;
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
        var category = await _categoryRepository.GetByIdAsync($"{request.CategoryId}", request.UserId, cancellationToken);
        if(category is null)
        {
            return new UpdateSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }

        var subcategory = request.ToModel(category);
        await _subcategoryRepository.UpdateAsync(subcategory, cancellationToken);
        await _mediator.Publish(new SubCategoryWasUpdated(subcategory));
        return new UpdateSubcategoryResponse(subcategory);
    }
}