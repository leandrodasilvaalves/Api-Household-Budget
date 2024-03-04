using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;


namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryHandler : ICreateCategoryHandler
{
    private readonly ICategoryData _Data;

    public CreateCategoryHandler(ICategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<CreateCategoryResponse> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category();
        category.Create(request);
        await _Data.CreateAsync(category, cancellationToken);
        return new CreateCategoryResponse(category);
    }
}