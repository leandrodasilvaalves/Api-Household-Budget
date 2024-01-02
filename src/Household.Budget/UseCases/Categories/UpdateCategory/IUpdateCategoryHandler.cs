namespace Household.Budget.UseCases.Categories.UpdateCategory;

public interface IUpdateCategoryHandler
{
    Task<UpdateCategoryResponse> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken);
}
