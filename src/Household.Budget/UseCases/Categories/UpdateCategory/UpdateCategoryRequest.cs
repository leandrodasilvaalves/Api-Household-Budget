using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
{
    public Guid Id { get; private set; }
    public string? Name { get; set; }
    public ModelStatus Status { get; set; }
    public ModelOwner Owner { get; set; }
    public Guid UserId { get; set; }

    public Category ToModel() => new()
    {
        Id = $"{Id}",
        Name = Name,
        Owner = Owner,
        Status = Status,
        UserId = $"{UserId}",
        UpdatedAt = DateTime.UtcNow,
    };

    public UpdateCategoryRequest WithId(Guid id)
    {
        Id = id;
        return this;
    }
}