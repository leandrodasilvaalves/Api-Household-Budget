namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public interface IUpdateSubcategoryHandler
{
    Task<UpdateSubcategoryResponse> Handle(UpdateSubcategoryRequest request, CancellationToken cancellationToken);
}
