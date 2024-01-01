using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;

public interface IDetachSubcategoryEventHandler
{
    Task Handle(SubCategoryWasExcluded notification, CancellationToken cancellationToken);
}
