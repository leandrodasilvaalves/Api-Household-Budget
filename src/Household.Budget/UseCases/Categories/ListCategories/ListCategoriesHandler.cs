using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Categories.ListCategories;

public class ListCategoriesHandler : IListCategoriesHandler
{
    private readonly ICategoryData _categoryData;

    public ListCategoriesHandler(ICategoryData categoryData)
    {
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
    }

    public async Task<ListCategoriesResponse> HandleAsync(ListCategoriesRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryData.GetAllAsync(request.PageSize, request.PageNumber, request.UserId, cancellationToken);
        return new ListCategoriesResponse(result);
    }
}
