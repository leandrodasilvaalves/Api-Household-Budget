using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Models;

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

        public Task Handle(SubcategoryChangedCategory notification, CancellationToken cancellationToken)
        {
            var subcategory = notification.Data;

            var taskNewCategory = HandleNewCategory(subcategory, cancellationToken);
            var taskOlCategory = HandleOldCategory(subcategory, notification.OldCategoryId, cancellationToken);
            Task.WaitAll([taskNewCategory, taskOlCategory], cancellationToken);
            return Task.CompletedTask;
        }

        public async Task HandleNewCategory(Subcategory subcategory, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(subcategory.Category.Id ?? "", subcategory.UserId ?? "", cancellationToken);
            if (category is not null)
            {
                category.Subcategories.Add(new(subcategory.Id, subcategory.Name));
                await _repository.UpdateAsync(category, cancellationToken);
            }
        }

        public async Task HandleOldCategory(Subcategory subcategory, string oldCategoryId, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(oldCategoryId ?? "", subcategory.UserId ?? "", cancellationToken);
            if (category is not null)
            {
                category.Subcategories.RemoveAll(x => x.Id == subcategory.Id);
                await _repository.UpdateAsync(category, cancellationToken);
            }
        }
    }
}