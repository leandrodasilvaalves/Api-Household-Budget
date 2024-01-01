namespace Household.Budget.UseCases.Categories.GetCategoryById;

public interface IGetCategoryByIdHandler
{
    Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken);
}
