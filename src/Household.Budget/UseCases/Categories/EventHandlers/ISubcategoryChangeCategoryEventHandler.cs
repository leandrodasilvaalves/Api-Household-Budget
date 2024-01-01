using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.Categories.EventHandlers;

public interface ISubcategoryChangeCategoryEventHandler
{
    Task Handle(SubcategoryChangedCategory notification, CancellationToken cancellationToken);
}
