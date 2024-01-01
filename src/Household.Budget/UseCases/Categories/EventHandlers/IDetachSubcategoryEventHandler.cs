using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers;

public interface IDetachSubcategoryEventHandler
{
    Task Handle(SubCategoryWasExcluded notification, CancellationToken cancellationToken);
}
