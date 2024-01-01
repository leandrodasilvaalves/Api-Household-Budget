namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public interface IGetSubcategoryByIdHandler
{
    Task<GetSubcategoryByIdResponse> Handle(GetSubcategoryByIdRequest request, CancellationToken cancellationToken);
}
