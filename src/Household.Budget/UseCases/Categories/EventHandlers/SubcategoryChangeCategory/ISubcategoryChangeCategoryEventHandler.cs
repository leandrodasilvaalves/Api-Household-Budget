using Household.Budget.Contracts.Events;

namespace Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

public interface ISubcategoryChangeCategoryEventHandler
{
    Task HandleAsync(SubcategoryChangedCategory notification, CancellationToken cancellationToken);
}
