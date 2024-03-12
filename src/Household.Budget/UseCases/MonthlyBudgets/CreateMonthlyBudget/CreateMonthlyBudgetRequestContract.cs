using Flunt.Notifications;
using Flunt.Validations;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;
using Household.Budget.Domain.Entities;
using Household.Budget.Contracts.Helpers;

namespace Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

public class CreateMonthlyBudgetRequestContract : Contract<CreateMonthlyBudgetRequest>
{
    public CreateMonthlyBudgetRequestContract(CreateMonthlyBudgetRequest request)
    {
        Requires()
            .IsTrue(request.Categories.All(x => !string.IsNullOrWhiteSpace(x.Id)),
                CategoryErrors.CATEGORY_ID_IS_REQUIRED)
            .IsTrue(request.Categories.All(x => x.Subcategories.All(x => !string.IsNullOrWhiteSpace(x.Id))),
                SubcategoryErrors.SUBCATEGORY_ID_IS_REQUIRED)
            .IsTrue(request.Categories.All(x => x.PlannedTotal == x.Subcategories.Sum(x => x.PlannedTotal)),
                BudgetError.CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES)
            .IsTrue(Year.IsValid(request.Year), CommonErrors.INVALID_YEAR);
    }

    public CreateMonthlyBudgetRequestContract(CreateMonthlyBudgetRequest request, List<Category> categories)
    {
        foreach (var requestCategory in request.Categories)
        {
            var category = categories?.FirstOrDefault(x => x.Id == requestCategory.Id);
            if (category is { })
            {
                foreach (var requestSubcategory in requestCategory.Subcategories)
                {
                    var subcategory = category.Subcategories?.FirstOrDefault(x => x.Id == requestSubcategory.Id);
                    if (subcategory?.Id is null)
                    {
                        AddNotification(SubcategoryErrors.SUBCATEGORY_NOT_FOUND, requestSubcategory.Id);
                    }
                }
            }
            else
            {
                AddNotification(CategoryErrors.CATEGORY_NOT_FOUND, requestCategory.Id);
            }
        }
    }

    private void AddNotification(Notification notification, string id)
    {
        AddNotification(notification.Key, $"{notification.Message}. Id: {id}");
    }
}