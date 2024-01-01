using Household.Budget.Contracts.Data;


namespace Household.Budget.UseCases.Categories.GetCategoryById;

public interface IGetCategoryByIdHandler
{
    Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken);
}

public class GetCategoryByIdHandler : IGetCategoryByIdHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return new GetCategoryByIdResponse(category);
    }
}
