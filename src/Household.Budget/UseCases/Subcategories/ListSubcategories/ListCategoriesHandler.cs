using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesHandler : IListSubcategoriesHandler
{
    private readonly ISubcategoryData _data;

    public ListSubcategoriesHandler(ISubcategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<ListSubcategoriesResponse> HandleAsync(ListSubcategoriesRequest request, CancellationToken cancellationToken)
    {
        var result = await _data.GetAllAsync(request.PageSize, request.PageNumber, request.UserId, cancellationToken);
        return new ListSubcategoriesResponse(result);
    }
}
