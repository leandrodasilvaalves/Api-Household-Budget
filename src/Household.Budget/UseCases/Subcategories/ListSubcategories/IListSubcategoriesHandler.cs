namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public interface IListSubcategoriesHandler
{
    Task<ListSubcategoriesResponse> Handle(ListSubcategoriesRequest request, CancellationToken cancellationToken);
}
