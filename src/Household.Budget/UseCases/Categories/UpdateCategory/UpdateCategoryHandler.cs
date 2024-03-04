using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryData _Data;

    public UpdateCategoryHandler(ICategoryData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<UpdateCategoryResponse> HandleAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await  _Data.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        if (category is null)
        {
            return new UpdateCategoryResponse(CategoryErrors.CATEGORY_NOT_FOUND);
        }
        
        category.Update(request);
        await _Data.UpdateAsync(category, cancellationToken);
        return new UpdateCategoryResponse(category);
    }
}
