using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public interface IAttachSubcategoryEventHandler
{
    Task Handle(SubcategoryWasCreated notification, CancellationToken cancellationToken);
}
