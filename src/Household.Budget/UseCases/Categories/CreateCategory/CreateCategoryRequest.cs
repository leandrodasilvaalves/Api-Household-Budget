using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequest : Request, IRequest<CreateCategoryResponse>
{
    public CreateCategoryRequest(string name, ModelType type = default)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; }

    public ModelType Type { get; }

    public override void Validate() =>
        AddNotifications(new CreateCategoryRequestContract(this));

    public Category ToModel() => new()
    {
        Id = $"{Guid.NewGuid()}",
        Name = Name,
        Type = Type,
        Status = ModelStatus.ACTIVE,
        UserId = Guid.TryParse(UserId, out var userId) ? userId : default,
        UpdatedAt = DateTime.UtcNow,
    };
}
