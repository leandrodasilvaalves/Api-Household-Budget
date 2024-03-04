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
        return new GetCategoryByIdResponse(category);
    }
}
