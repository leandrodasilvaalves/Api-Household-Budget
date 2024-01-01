using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;


namespace Household.Budget.UseCases.Categories.EventHandlers;

public class DetachSubcategoryEventHandler : IDetachSubcategoryEventHandler
{
    private readonly ICategoryRepository _repository;

    public DetachSubcategoryEventHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(SubCategoryWasExcluded notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;
        var category = await _repository.GetByIdAsync(
            subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);
        if (category is not null)
        {
            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _repository.UpdateAsync(category, cancellationToken);
        }
    }
}