using Flunt.Notifications;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using MediatR;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryRequest : Notifiable<Notification>, IRequest<CreateCategoryResponse>
{
    public CreateCategoryRequest(string name, ModelType type = default)
    {
        Name = name;
        Type = type;
        AddNotifications(new CreateCategoryRequestContract(this));
    }

    public string Name { get; }
    public ModelType Type { get; }

    public Category ToModel() => new()
    {
        Id = $"{Guid.NewGuid()}",
        Name = Name,
        Type = Type,
        Status = ModelStatus.ACTIVE,
        UserId = Guid.NewGuid(),//TODO: obter ID do usuario logado
        UpdatedAt = DateTime.UtcNow,
    };
}
