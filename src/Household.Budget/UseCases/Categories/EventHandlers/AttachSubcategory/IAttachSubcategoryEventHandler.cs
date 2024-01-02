using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public interface IAttachSubcategoryEventHandler
{
    Task<SubcategoryWasCreatedEventResponse> HandleAsync(SubcategoryWasCreated notification, CancellationToken cancellationToken);
}
