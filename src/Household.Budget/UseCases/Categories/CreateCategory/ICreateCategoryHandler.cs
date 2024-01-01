namespace Household.Budget.UseCases.Categories.CreateCategories;

public interface ICreateCategoryHandler
{
    Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
}
