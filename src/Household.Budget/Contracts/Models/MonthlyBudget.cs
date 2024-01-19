using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

namespace Household.Budget.Contracts.Models;

public class MonthlyBudget : Model
{
    public MonthlyBudget()
    {
        Owner = ModelOwner.USER;
        var utcNow = DateTime.UtcNow;
        Year = utcNow.Year;
        Month = (Month)utcNow.Month;
        Categories = [];
    }

    public int Year { get; set; }
    public Month Month { get; set; }
    public List<BudgetCategoryModel> Categories { get; set; }

    public TotalModel? Incomes { get; set; }
    public TotalModel? Expenses { get; set; }

    public void Create(CreateMonthlyBudgetRequest request, List<Category> categories)
    {
        Id = $"{Guid.NewGuid()}";
        Year = request.Year;
        Month = request.Month;
        UserId = request.UserId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        AddCategories(request.Categories, categories);
        Incomes = new(Categories.Where(c => c.Type == CategoryType.INCOMES).ToList());
        Expenses = new(Categories.Where(c => c.Type == CategoryType.EXPENSES).ToList());
    }

    private void AddCategories(List<BudgetCategoryRequestViewModel> categoriesRequest, List<Category> categories)
    {
        foreach (var category in categories)
        {
            var request = categoriesRequest.FirstOrDefault(x => x.Id == category?.Id);
            var budgetCategory = new BudgetCategoryModel
            {
                Id = category?.Id ?? "",
                Name = category?.Name ?? "",
                PlannedTotal = request?.PlannedTotal ?? 0,
                Type = category?.Type,
                Subcategories = AddSubCategories(request?.Subcategories, category?.Subcategories)
            };
            Categories.Add(budgetCategory);
        }
    }

    private static List<BudgetSubcategoryModel> AddSubCategories(List<BudgetCategoryRequestViewModel>? subcategoriesRequest,
                                                          List<CategoryBranch>? subcategories)
    {
        List<BudgetSubcategoryModel> subcategoriesModel = [];
        foreach (var subcategory in subcategories ?? [])
        {
            var request = subcategoriesRequest?.FirstOrDefault(x => x.Id == subcategory.Id);
            var subcategoryModel = new BudgetSubcategoryModel
            {
                Id = subcategory.Id ?? "",
                Name = subcategory.Name ?? "",
                PlannedTotal = request?.PlannedTotal ?? 0,
            };
            subcategoriesModel.Add(subcategoryModel);
        }
        return subcategoriesModel;
    }
}

public class BudgetCategoryModel
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public CategoryType? Type { get; set; }
    public float PlannedTotal { get; set; }
    public float ActualTotal { get; set; }
    public float Difference => PlannedTotal - ActualTotal;
    public List<BudgetSubcategoryModel>? Subcategories { get; set; }
}

public class BudgetSubcategoryModel : BudgetCategoryModel
{
    public List<BudgetSubcategoryModel>? Transactions { get; set; }
}

public class BudgetTransactionModel
{
    public string Id { get; set; } = "";
    public float Total { get; set; }
}

public class TotalModel(List<BudgetCategoryModel> categories)
{
    public float PlannedTotal { get; set; } = categories.Sum(x => x.PlannedTotal);
    public float ActualTotal { get; set; } = categories.Sum(x => x.ActualTotal);
}