using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

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
    public TotalModel Incomes { get; set; } = new();
    public TotalModel Expenses { get; set; } = new();

    public void Create(CreateMonthlyBudgetRequest request, List<Category> categories)
    {
        Id = $"{Guid.NewGuid()}";
        Year = request.Year;
        Month = request.Month;
        UserId = request.UserId;
        CreatedAt = DateTime.UtcNow;
        AddCategories(request.Categories, categories);
        Incomes.Calculate(GetTotals(CategoryType.INCOMES));
        Expenses.Calculate(GetTotals(CategoryType.EXPENSES));
        UpdatedAt = DateTime.UtcNow;
    }

    public void Merge(UpdateMonthlyBudgetRequest request)
    {
        if (request.Status.HasValue)
        {
            Status = request.Status.Value;
            if (Status == ModelStatus.EXCLUDED)
            {
                return;
            }
        }
        if (request.Year.HasValue)
        {
            Year = request.Year.Value;
        }
        if (request.Month.HasValue)
        {
            Month = request.Month.Value;
        }
        MergeCategories(request.Categories);
        Incomes.Calculate(GetTotals(CategoryType.INCOMES));
        Expenses.Calculate(GetTotals(CategoryType.EXPENSES));
        UpdatedAt = DateTime.UtcNow;
    }

    private void MergeCategories(List<BudgetCategoryRequestViewModel> categoriesRequest)
    {
        foreach (var category in categoriesRequest)
        {
            var budgetCategory = Categories.FirstOrDefault(x => x.Id == category.Id);
            MergeSubcategories(category.Subcategories, budgetCategory?.Subcategories ?? []);
            budgetCategory?.UpdatePlannedTotal(category);
        }
    }

    private static void MergeSubcategories(List<BudgetCategoryRequestViewModel> subcategoriesRequest,
                                           List<BudgetSubcategoryModel> budgetSubcategories)
    {
        foreach (var subcategoryRequest in subcategoriesRequest)
        {
            var budgetSubcategory = budgetSubcategories.FirstOrDefault(x => x.Id == subcategoryRequest.Id);
            budgetSubcategory?.UpdatePlannedTotal(subcategoryRequest);
        }
    }

    private IEnumerable<TotalModel> GetTotals(CategoryType type) =>
        from categ in Categories where categ.Type == type select categ.Total;

    private void AddCategories(List<BudgetCategoryRequestViewModel> categoriesRequest, List<Category> categories)
    {
        foreach (var category in categories)
        {
            var request = categoriesRequest.FirstOrDefault(x => x.Id == category?.Id);
            var budgetCategory = new BudgetCategoryModel
            {
                Id = category?.Id ?? "",
                Name = category?.Name ?? "",
                Total = (TotalModel)request?.PlannedTotal,
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
                Total = (TotalModel)request?.PlannedTotal,
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
    public TotalModel? Total { get; set; }
    public List<BudgetSubcategoryModel>? Subcategories { get; set; }

    public void UpdatePlannedTotal(BudgetCategoryRequestViewModel subcategoryRequest) =>
        Total = (TotalModel)subcategoryRequest.PlannedTotal;
}

public class BudgetSubcategoryModel : BudgetCategoryModel
{
    public List<BudgetTransactionModel>? Transactions { get; set; }
}

public class BudgetTransactionModel
{
    public string Id { get; set; } = "";
    public float Amount { get; set; }
}

public class TotalModel
{
    public float Planned { get; set; }
    public float Actual { get; set; }
    public float Difference => Planned - Actual;
    public string Percentage => (Planned == 0 ? 0 : Actual / Planned * 100).ToString("P2");

    public void Calculate(IEnumerable<TotalModel> totals)
    {
        Planned = totals.Sum(x => x.Planned);
        Actual = totals.Sum(x => x.Actual);
    }

    public static explicit operator TotalModel(float? planned) =>
        new() { Planned = planned ?? 0, Actual = 0 };
}