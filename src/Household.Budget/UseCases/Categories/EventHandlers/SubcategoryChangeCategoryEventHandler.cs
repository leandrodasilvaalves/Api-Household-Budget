using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;

using MediatR;

namespace Household.Budget.UseCases.Categories.EventHandlers
{
    public class SubcategoryChangeCategoryEventHandler : INotificationHandler<SubcategoryChangedCategory>
    {
        private readonly ICategoryRepository _repository;

        public SubcategoryChangeCategoryEventHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(SubcategoryChangedCategory notification, CancellationToken cancellationToken)
        {
            var subcategory = notification.Data;

            var newCategory = await _repository.GetByIdAsync(
                subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);

            newCategory.Subcategories.Add(new(subcategory.Id, subcategory.Name));
            await _repository.UpdateAsync(newCategory, cancellationToken);

            var oldCategory = await _repository.GetByIdAsync(
                notification.OldCategoryId ?? "", subcategory.UserId ?? "", cancellationToken);
            oldCategory.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
            await _repository.UpdateAsync(oldCategory, cancellationToken);
        }
    }
}