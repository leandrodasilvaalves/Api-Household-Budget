using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryData _data;

    public UpdateCategoryHandler(ICategoryData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<UpdateCategoryResponse> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await  _data.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        if (category is null)
        {
            return new UpdateCategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        
        category.Update(request);
        await _data.UpdateAsync(category, cancellationToken);
        return new UpdateCategoryResponse(category);
    }
}
