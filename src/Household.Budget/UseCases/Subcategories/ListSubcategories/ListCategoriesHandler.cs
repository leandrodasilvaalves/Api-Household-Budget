using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesHandler : IListSubcategoriesHandler
{
    private readonly ISubcategoryData _Data;

    public ListSubcategoriesHandler(ISubcategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<ListSubcategoriesResponse> HandleAsync(ListSubcategoriesRequest request, CancellationToken cancellationToken)
    {
        var result = await _Data.GetAllAsync(request.PageSize, request.PageNumber, request.UserId, cancellationToken);
        return new ListSubcategoriesResponse(result);
    }
}
