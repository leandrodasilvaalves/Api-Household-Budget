using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public interface IAttachSubcategoryEventHandler
{
    Task<AttachSubcategoryResponse> HandleAsync(SubcategoryWasCreated notification, CancellationToken cancellationToken);
}
