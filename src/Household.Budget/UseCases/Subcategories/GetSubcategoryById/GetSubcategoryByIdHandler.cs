using Household.Budget.Contracts.Data;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdHandler : IGetSubcategoryByIdHandler
{
    private readonly ISubcategoryRepository _repository;

    public GetSubcategoryByIdHandler(ISubcategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetSubcategoryByIdResponse> HandleAsync(GetSubcategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var subcategory = await _repository.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return new GetSubcategoryByIdResponse(subcategory);
    }
}
