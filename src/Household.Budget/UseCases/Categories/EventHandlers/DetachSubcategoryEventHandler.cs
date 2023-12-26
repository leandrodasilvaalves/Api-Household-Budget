using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;

using MediatR;

namespace Household.Budget.UseCases.Categories.EventHandlers;

public class DetachSubcategoryEventHandler : INotificationHandler<SubCategoryWasUpdated>
{
    private readonly ICategoryRepository _repository;

    public DetachSubcategoryEventHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(SubCategoryWasUpdated notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;
        if (subcategory.Status == Contracts.Enums.ModelStatus.EXCLUDED)
        {
            var category = await _repository.GetByIdAsync(
                subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);

            category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _repository.UpdateAsync(category, cancellationToken);
        }
    }
}