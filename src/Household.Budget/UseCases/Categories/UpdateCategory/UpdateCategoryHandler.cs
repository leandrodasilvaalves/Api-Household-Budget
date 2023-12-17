using Household.Budget.Contracts.Data;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = request.ToModel();
        await _categoryRepository.UpdateAsync(category, cancellationToken);
        return new UpdateCategoryResponse(category);
    }
}
