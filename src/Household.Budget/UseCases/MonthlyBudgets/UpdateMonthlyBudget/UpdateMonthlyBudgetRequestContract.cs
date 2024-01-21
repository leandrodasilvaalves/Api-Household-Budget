using Flunt.Validations;

using Household.Budget.Contracts;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

public class UpdateMonthlyBudgetRequestContract : Contract<UpdateMonthlyBudgetRequest>
{
    public UpdateMonthlyBudgetRequestContract(UpdateMonthlyBudgetRequest request)
    {
        Requires()
            .IsTrue(request.Categories.All(x => !string.IsNullOrWhiteSpace(x.Id)),
                CategoryErrors.CATEGORY_ID_IS_REQUIRED)
            .IsTrue(request.Categories.All(x => x.Subcategories.All(x => !string.IsNullOrWhiteSpace(x.Id))),
                SubcategoryErrors.SUBCATEGORY_ID_IS_REQUIRED)
            .IsTrue(request.Categories.All(x => x.PlannedTotal == x.Subcategories.Sum(x => x.PlannedTotal)),
                BudgetError.CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES)
            .IsSatified(request, 
                x => x.Year.HasValue, 
                x => CurrentYear.IsValid(x?.Year ?? 0), 
                CommonErrors.INVALID_YEAR);
    }

    public UpdateMonthlyBudgetRequestContract(MonthlyBudget monthlyBudget,
                                              bool alreadyExists,
                                              UpdateMonthlyBudgetRequest request)
    {
        if (monthlyBudget is null)
        {
            AddNotification(BudgetError.BUGET_NOT_FOUND);
        };
        if (alreadyExists)
        {
            AddNotification(BudgetError.BUGET_ALREADY_EXISTS);
        }

        if (monthlyBudget is { })
        {
            foreach (var requestCategory in request.Categories)
            {
                var dbCategory = monthlyBudget.Categories.FirstOrDefault(x => x.Id == requestCategory.Id);
                if (requestCategory.PlannedTotal < dbCategory?.Subcategories?.Sum(x => x.Transactions?.Sum(t => t?.Amount)))
                {
                    AddNotification(BudgetError.NEW_PLANNED_TOTAL_MUST_BE_GREATER_OR_EQUAL_TO_ACTUAL_TOTAL);
                }
            }
        }
    }
}