namespace Household.Budget.UseCases.Categories.UpdateCategory;

public interface IUpdateCategoryHandler
{
    Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken);
}
