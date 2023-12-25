using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;

using MediatR;

namespace Household.Budget.UseCases.Categories.AttachSubcategory;

public class AttachSubCategoryHandler : INotificationHandler<SubCategoryWasCreated>
{
    private readonly ICategoryRepository _repository;

    public AttachSubCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(SubCategoryWasCreated notification, CancellationToken cancellationToken)
    {
        var subcategory = notification.Data;
        var category = await _repository.GetByIdAsync($"{subcategory.Category.Id}",
            subcategory.UserId ?? "", cancellationToken);

        if (category is not null)
        {
            category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _repository.UpdateAsync(category, cancellationToken);
        }
    }
}