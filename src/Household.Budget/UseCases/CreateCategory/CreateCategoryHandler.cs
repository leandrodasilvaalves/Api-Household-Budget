using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = $"{Guid.NewGuid()}",
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Type = ModelType.SYSTEM,
        };
        
        await _repository.CreateAsync(category, cancellationToken);
        return new CreateCategoryResponse(category);
    }
}
