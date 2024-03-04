using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdHandler : IGetSubcategoryByIdHandler
{
    private readonly ISubcategoryData _Data;

    public GetSubcategoryByIdHandler(ISubcategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<GetSubcategoryByIdResponse> HandleAsync(GetSubcategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var subcategory = await _Data.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return new GetSubcategoryByIdResponse(subcategory);
    }
}
