using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers;

public interface IAttachSubCategoryEventHandler
{
    Task Handle(SubcategoryWasCreated notification, CancellationToken cancellationToken);
}
