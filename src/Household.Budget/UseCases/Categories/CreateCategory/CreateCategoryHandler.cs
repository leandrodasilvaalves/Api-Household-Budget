using Household.Budget.Contracts.Data;

using MediatR;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        if(request.IsValid is false)
        {
            return new CreateCategoryResponse(request.Notifications);
        }

        var category = request.ToModel();
        await _repository.CreateAsync(category, cancellationToken);
        return new CreateCategoryResponse(category);
    }
}