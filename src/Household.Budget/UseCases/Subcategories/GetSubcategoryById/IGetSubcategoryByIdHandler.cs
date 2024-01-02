namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public interface IGetSubcategoryByIdHandler
{
    Task<GetSubcategoryByIdResponse> HandleAsync(GetSubcategoryByIdRequest request, CancellationToken cancellationToken);
}
