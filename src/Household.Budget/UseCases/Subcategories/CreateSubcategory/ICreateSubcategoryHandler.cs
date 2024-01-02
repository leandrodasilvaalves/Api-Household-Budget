namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public interface ICreateSubcategoryHandler
{
    Task<CreateSubcategoryResponse> HandleAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken);
}
