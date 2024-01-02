namespace Household.Budget.UseCases.Categories.GetCategoryById;

public interface IGetCategoryByIdHandler
{
    Task<GetCategoryByIdResponse> HandleAsync(GetCategoryByIdRequest request, CancellationToken cancellationToken);
}
