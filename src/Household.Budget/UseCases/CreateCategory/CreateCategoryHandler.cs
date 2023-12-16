using Household.Budget.Contracts.Data;
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
        if(request.IsValid == false)
        {
            return new CreateCategoryResponse(request.Notifications);
        }

        var category = new Category
        {
            Id = $"{Guid.NewGuid()}",
            Name = request.Name,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        
        await _repository.CreateAsync(category, cancellationToken);
        return new CreateCategoryResponse(category);
    }
}
