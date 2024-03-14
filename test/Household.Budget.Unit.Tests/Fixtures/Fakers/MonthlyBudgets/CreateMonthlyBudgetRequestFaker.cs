using AutoFixture;

using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.MonthlyBudgets;

public class CreateMonthlyBudgetRequestFaker : CreateMonthlyBudgetRequest
{
    private readonly Random _random;
    public CreateMonthlyBudgetRequestFaker(IFixture fixture)
    {
        var today = DateTime.Now;
        Year = today.Year;
        Month = (Month)today.Month;
        _random = new Random(today.Millisecond);

        var categories = fixture.CreateMany<BudgetCategoryRequestViewModel>().ToList();
        var plannedTotal = categories.Sum(s => s.PlannedTotal);
        Categories = categories;
        Categories.ForEach(c =>
        {
            c.Subcategories.AddRange(categories);
            c.PlannedTotal = plannedTotal;
        });
    }

    public CreateMonthlyBudgetRequest WithCategoryIdNull()
    {
        RandomCategory().Id = null;
        return this;
    }

    public CreateMonthlyBudgetRequest WithCategoryIdEmpty()
    {
        RandomCategory().Id = "";
        return this;
    }

    public CreateMonthlyBudgetRequest WithSubcategoryIdNull()

    {
        RandomSubcategory(RandomCategory()).Id = null;
        return this;
    }

    public CreateMonthlyBudgetRequest WithSubcategoryIdEmpty()

    {
        RandomSubcategory(RandomCategory()).Id = "";
        return this;
    }

    public CreateMonthlyBudgetRequest WithCategoryInvalidPlannedTotal()
    {
        RandomCategory().PlannedTotal *= 0.9m;
        return this;
    }

    public CreateMonthlyBudgetRequest WithInvalidYear()
    {
        Year -= Contracts.Helpers.Year.Min + 1;
        return this;
    }

    private BudgetCategoryRequestViewModel RandomCategory() =>
        Categories[_random.Next(0, Categories.Count - 1)];

    private BudgetCategoryRequestViewModel RandomSubcategory(BudgetCategoryRequestViewModel category) =>
        category.Subcategories[_random.Next(0, category.Subcategories.Count - 1)];
}