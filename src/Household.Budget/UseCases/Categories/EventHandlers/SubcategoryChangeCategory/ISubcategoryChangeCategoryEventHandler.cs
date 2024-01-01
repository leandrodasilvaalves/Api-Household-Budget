using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

public interface ISubcategoryChangeCategoryEventHandler
{
    Task Handle(SubcategoryChangedCategory notification, CancellationToken cancellationToken);
}
