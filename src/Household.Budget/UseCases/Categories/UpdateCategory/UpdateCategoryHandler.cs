using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Errors;

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
        var category = await  _repository.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        if (category is null)
        {
            return new UpdateCategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        
        category.Update(request);
        await _repository.UpdateAsync(category, cancellationToken);
        return new UpdateCategoryResponse(category);
    }
}
