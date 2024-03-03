using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UpdateCategoryResponse> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category();
        category.Update(request);
        await _repository.UpdateAsync(category, cancellationToken);
        return new UpdateCategoryResponse(category);
    }
}
