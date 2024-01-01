using Household.Budget.Contracts.Data;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = request.ToModel();
        await _repository.UpdateAsync(category, cancellationToken);
        return new UpdateCategoryResponse(category);
    }
}
