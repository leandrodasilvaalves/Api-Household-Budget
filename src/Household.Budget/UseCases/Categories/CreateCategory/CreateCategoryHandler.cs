using Household.Budget.Contracts.Data;


namespace Household.Budget.UseCases.Categories.CreateCategories;


public interface ICreateCategoryHandler
{
    Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
}

public class CreateCategoryHandler : ICreateCategoryHandler
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = request.ToModel();
        await _repository.CreateAsync(category, cancellationToken);
        return new CreateCategoryResponse(category);
    }
}