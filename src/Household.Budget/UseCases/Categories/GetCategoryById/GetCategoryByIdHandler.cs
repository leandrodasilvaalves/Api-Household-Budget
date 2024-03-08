using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;


namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdHandler : IGetCategoryByIdHandler
{
    private readonly ICategoryData _categoryData;

    public GetCategoryByIdHandler(ICategoryData categoryData)
    {
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
    }

    public async Task<GetCategoryByIdResponse> HandleAsync(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryData.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return category == null ? 
            new GetCategoryByIdResponse(CategoryErrors.CATEGORY_NOT_FOUND) : 
            new GetCategoryByIdResponse(category);
    }
}
