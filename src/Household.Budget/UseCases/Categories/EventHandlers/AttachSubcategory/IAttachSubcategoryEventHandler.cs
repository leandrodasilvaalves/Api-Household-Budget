using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public interface IAttachSubcategoryEventHandler
{
    Task<SubcategoryWasCreatedEventResponse> Handle(SubcategoryWasCreated notification, CancellationToken cancellationToken);
}
