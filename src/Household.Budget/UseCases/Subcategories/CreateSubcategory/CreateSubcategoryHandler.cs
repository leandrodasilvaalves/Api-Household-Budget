using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Entities;

using MassTransit;

namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public class CreateSubcategoryHandler : ICreateSubcategoryHandler
{
    private readonly ISubcategoryData _subcategoryData;
    private readonly ICategoryData _categoryData;
    private readonly IBus _bus;

    public CreateSubcategoryHandler(ISubcategoryData subcategoryData,
                                    ICategoryData categoryData,
                                    IBus bus)
    {
        _subcategoryData = subcategoryData ?? throw new ArgumentNullException(nameof(subcategoryData));
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<CreateSubcategoryResponse> HandleAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryData.GetByIdAsync($"{request.CategoryId}", request.UserId, cancellationToken);
        if (category is null)
        {
            return new CreateSubcategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        var subcategory = new Subcategory();
        subcategory.Create(request, category);
        
        await _subcategoryData.CreateAsync(subcategory, cancellationToken);
        await _bus.Publish(new SubcategoryWasCreated(subcategory), cancellationToken);
        return new CreateSubcategoryResponse(subcategory);
    }
}
