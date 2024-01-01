using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;

using MassTransit;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public interface ICreateSubcategoryHandler
{
    Task<CreateSubcategoryResponse> Handle(CreateSubcategoryRequest request, CancellationToken cancellationToken);
}

public class CreateSubcategoryHandler : ICreateSubcategoryHandler
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBus _bus;

    public CreateSubcategoryHandler(ISubcategoryRepository subcategoryRepository,
                                    ICategoryRepository categoryRepository,
                                    IBus bus)
    {
        _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
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
        await _bus.Publish(new SubcategoryWasCreated(subcategory), cancellationToken);
        return new CreateSubcategoryResponse(subcategory);
    }
}
