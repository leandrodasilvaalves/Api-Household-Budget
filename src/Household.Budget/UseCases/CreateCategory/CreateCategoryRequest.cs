using Flunt.Notifications;

using Household.Budget.Contracts.Enums;

using MediatR;

namespace Household.Budget.UseCases.CreateCategory;

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
}
