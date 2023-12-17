using Household.Budget.Contracts.Data;

using MediatR;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetCategoryByIdResponse(category);
    }
}
