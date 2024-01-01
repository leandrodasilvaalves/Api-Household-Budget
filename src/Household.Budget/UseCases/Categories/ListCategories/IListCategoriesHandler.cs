namespace Household.Budget.UseCases.Categories.ListCategories;

public interface IListCategoriesHandler
{
    Task<ListCategoriesResponse> Handle(ListCategoriesRequest request, CancellationToken cancellationToken);
}
