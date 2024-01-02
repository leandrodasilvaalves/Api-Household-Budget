namespace Household.Budget.UseCases.Categories.CreateCategories;

public interface ICreateCategoryHandler
{
    Task<CreateCategoryResponse> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken);
}
