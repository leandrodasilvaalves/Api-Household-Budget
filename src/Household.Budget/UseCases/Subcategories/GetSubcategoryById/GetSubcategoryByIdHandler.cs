using Household.Budget.Contracts.Data;

using MediatR;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdHandler : IRequestHandler<GetSubcategoryByIdRequest, GetSubcategoryByIdResponse>
{
    private readonly ISubcategoryRepository _repository;

    public GetSubcategoryByIdHandler(ISubcategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetSubcategoryByIdResponse> Handle(GetSubcategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var subcategory = await _repository.GetByIdAsync($"{request.SubcategoryId}", request.UserId, cancellationToken);
        return new GetSubcategoryByIdResponse(subcategory);
    }
}
