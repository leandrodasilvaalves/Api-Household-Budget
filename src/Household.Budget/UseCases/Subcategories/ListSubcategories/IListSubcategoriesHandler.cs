namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public interface IListSubcategoriesHandler
{
    Task<ListSubcategoriesResponse> HandleAsync(ListSubcategoriesRequest request, CancellationToken cancellationToken);
}
