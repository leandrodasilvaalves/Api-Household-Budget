using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;


namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryHandler : ICreateCategoryHandler
{
    private readonly ICategoryData _data;

    public CreateCategoryHandler(ICategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<CreateCategoryResponse> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category();
        category.Create(request);
        await _data.CreateAsync(category, cancellationToken);
        return new CreateCategoryResponse(category);
    }
}