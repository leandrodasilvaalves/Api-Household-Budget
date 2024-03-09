using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdHandler : IGetSubcategoryByIdHandler
{
    private readonly ISubcategoryData _data;

    public GetSubcategoryByIdHandler(ISubcategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<GetSubcategoryByIdResponse> HandleAsync(GetSubcategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var subcategory = await _data.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return subcategory == null
            ? new GetSubcategoryByIdResponse(SubcategoryErrors.SUBCATEGORY_NOT_FOUND)
            : new GetSubcategoryByIdResponse(subcategory);
    }
}
