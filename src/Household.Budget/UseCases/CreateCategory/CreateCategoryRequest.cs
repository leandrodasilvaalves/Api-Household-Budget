using MediatR;

namespace Household.Budget.UseCases.CreateCategory;

public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
{
    public string? Name { get; set; }
}
