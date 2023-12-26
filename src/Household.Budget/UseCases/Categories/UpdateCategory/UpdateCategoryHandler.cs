using Household.Budget.Contracts.Data;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
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
