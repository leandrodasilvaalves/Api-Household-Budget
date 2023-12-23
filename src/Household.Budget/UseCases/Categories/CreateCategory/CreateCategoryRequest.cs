using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequest : Request, IRequest<CreateCategoryResponse>
{
    public CreateCategoryRequest(string name, ModelOwner owner = default)
    {
        Name = name;
        Owner = owner;
    }

    public string Name { get; }

    public ModelOwner Owner { get; }

    public override void Validate() =>
        AddNotifications(new CreateCategoryRequestContract(this));

    public Category ToModel() => new()
    {
        Id = $"{Guid.NewGuid()}",
        Name = Name,
        Owner = Owner,
        Status = ModelStatus.ACTIVE,
        UserId = UserId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };
}
