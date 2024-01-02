namespace Household.Budget.UseCases.Categories.ListCategories;

public interface IListCategoriesHandler
{
    Task<ListCategoriesResponse> HandleAsync(ListCategoriesRequest request, CancellationToken cancellationToken);
}
