namespace Household.Budget.UseCases.Subcategories.CreateSubcategory;

public interface ICreateSubcategoryHandler
{
    Task<CreateSubcategoryResponse> Handle(CreateSubcategoryRequest request, CancellationToken cancellationToken);
}
