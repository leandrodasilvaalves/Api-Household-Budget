using AutoFixture;

using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Entities;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

namespace Household.Budget.Unit.Tests.Fixtures.Fakers.MonthlyBudgets
{
    public class UpdateMonthlyBudgetRequestFaker : UpdateMonthlyBudgetRequest
    {
        private readonly Random _random;
        public UpdateMonthlyBudgetRequestFaker(IFixture fixture)
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

        public UpdateMonthlyBudgetRequest WithCategoryIdNull()
        {
            RandomCategory().Id = null;
            return this;
        }

        public UpdateMonthlyBudgetRequest WithCategoryIdEmpty()
        {
            RandomCategory().Id = "";
            return this;
        }

        public UpdateMonthlyBudgetRequest WithSubcategoryIdNull()

        {
            RandomSubcategory(RandomCategory()).Id = null;
            return this;
        }

        public UpdateMonthlyBudgetRequest WithSubcategoryIdEmpty()

        {
            RandomSubcategory(RandomCategory()).Id = "";
            return this;
        }

        public UpdateMonthlyBudgetRequest WithCategoryInvalidPlannedTotal()
        {
            RandomCategory().PlannedTotal *= 0.9m;
            return this;
        }

        public UpdateMonthlyBudgetRequest WithCategoryInvalidPlannedTotal(MonthlyBudget monthlyBudget)
        {
            Categories = (from category in monthlyBudget.Categories
                          select new BudgetCategoryRequestViewModel
                          {
                              Id = category.Id,
                              PlannedTotal = category.Total.Actual * 0.95m
                          }).ToList();
            return this;
        }

        public UpdateMonthlyBudgetRequest WithInvalidYear()
        {
            Year -= Contracts.Helpers.Year.Min + 1;
            return this;
        }

        public UpdateMonthlyBudgetRequest WithNullYear()
        {
            Year = null;
            return this;
        }

        private BudgetCategoryRequestViewModel RandomCategory() =>
            Categories[_random.Next(0, Categories.Count - 1)];

        private BudgetCategoryRequestViewModel RandomSubcategory(BudgetCategoryRequestViewModel category) =>
            category.Subcategories[_random.Next(0, category.Subcategories.Count - 1)];
    }
}