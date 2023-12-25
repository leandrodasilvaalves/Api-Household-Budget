using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;

using MediatR;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public class CreateSubcategoryHandler : IRequestHandler<CreateSubcategoryRequest, CreateSubcategoryResponse>
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMediator _mediator;

    public CreateSubcategoryHandler(ISubcategoryRepository subcategoryRepository,
                                    ICategoryRepository categoryRepository,
                                    IMediator mediator)
    {
        _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mediator = mediator;
    }

    public async Task<CreateSubcategoryResponse> Handle(CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync($"{request.CategoryId}", request.UserId, cancellationToken);
        if (category is null)
        {
            return new CreateSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        var subcategory = request.ToModel(category);
        await _subcategoryRepository.CreateAsync(subcategory, cancellationToken);
        await _mediator.Publish(new SubCategoryWasCreated(subcategory), cancellationToken);
        return new CreateSubcategoryResponse(subcategory);
    }
}
