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

    public void AttachTransaction(Transaction? transaction)
    {
        Categories?.FirstOrDefault(c => c.Id == transaction?.Category.Id)
            ?.Subcategories?.FirstOrDefault(s => s.Id == transaction?.Category?.Subcategory?.Id)
                ?.AddTransaction((BudgetTransactionModel)transaction);

        Recalculate();
    }

    public void AttachTransaction(BudgetTransactionWithCategoryModel? transaction)
    {
        Categories?.FirstOrDefault(c => c.Id == transaction?.Category)
            ?.Subcategories?.FirstOrDefault(s => s.Id == transaction?.Subcategory)
                ?.AddTransaction(transaction);

        Recalculate();
    }

    public void DetachTransaction(Transaction transaction)
    {
        Categories?.FirstOrDefault(c => c.Id == transaction.Category.Id)
            ?.Subcategories?.FirstOrDefault(s => s.Id == transaction.Category?.Subcategory?.Id)
                ?.RemoveTransaction(x => x.Id == transaction.Id);

        Recalculate();
    }

    public void UpdateTransacation(Transaction transaction)
    {
        var originalCategory = (from categ in Categories
                                from subcateg in categ.Subcategories ?? []
                                from trans in subcateg.Transactions ?? []
                                where trans.Id == transaction.Id
                                select new { Id = categ.Id, subcategId = subcateg.Id })
                       .FirstOrDefault();

        if (originalCategory is not null && originalCategory?.subcategId != transaction?.Category?.Subcategory?.Id)
        {
            Categories.FirstOrDefault(c => c.Id == originalCategory?.Id)
                ?.Subcategories?.FirstOrDefault(s => s?.Id == originalCategory?.subcategId)
                    ?.RemoveTransaction(t => t.Id == transaction?.Id);

            AttachTransaction(transaction);
        }
        else
        {
            Categories?.FirstOrDefault(c => c?.Id == transaction?.Category.Id)
                ?.Subcategories?.FirstOrDefault(s => s?.Id == transaction?.Category?.Subcategory?.Id)
                ?.UpdateTransaction(transaction);

            Recalculate();
        }
    }

    private void Recalculate()
    {
        Categories?.ForEach(c => c.CalculateTotal());
        Incomes.Calculate(GetTotals(CategoryType.INCOMES));
        Expenses.Calculate(GetTotals(CategoryType.EXPENSES));
        UpdatedAt = DateTime.UtcNow;
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

    public void CalculateTotal()
    {
        Total ??= new();
        Total.Actual = Subcategories?.Sum(x => x.Total?.Actual ?? 0) ?? 0;
    }
}

public class BudgetSubcategoryModel : BudgetCategoryModel
{
    public List<BudgetTransactionModel>? Transactions { get; set; }
    public void AddTransaction(BudgetTransactionModel? transaction)
    {
        Transactions ??= [];
        Transactions.Add(transaction);
        Recalculate();
    }

    public void RemoveTransaction(Predicate<BudgetTransactionModel> predicate)
    {
        Transactions?.RemoveAll(predicate);
        Recalculate();
    }

    public void UpdateTransaction(Transaction? transaction)
    {
        var index = Transactions?.FindIndex(t => t.Id == transaction?.Id);
        if (index.HasValue)
        {
            Transactions ??= [];
            Transactions[index.Value] = (BudgetTransactionModel)transaction;
        }
    }

    private void Recalculate()
    {
        Total ??= new();
        Total.Actual = Transactions?.Sum(x => x.Amount) ?? 0;
    }
}

public class BudgetTransactionModel
{
    public string Id { get; set; } = "";
    public DateTime? TransactionDate { get; set; }
    public string? Description { get; set; } = "";
    public decimal Amount { get; set; }

    public static explicit operator BudgetTransactionModel(Transaction? transaction) => new()
    {
        Id = transaction?.Id ?? "",
        Description = Reduce(transaction?.Description ?? ""),
        TransactionDate = transaction?.TransactionDate,
        Amount = transaction?.Payment?.Total ?? 0
    };

    protected static string Reduce(string value, short maxLength = 30) =>
        value.Length > maxLength ? value[..maxLength] : value;
}

public class BudgetTransactionWithCategoryModel : BudgetTransactionModel
{
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public string UserId { get; set; } = "";

    public (int?, Month) GetYearMonth() => 
        (TransactionDate?.Year, (Month)TransactionDate?.Month);

    public static explicit operator BudgetTransactionWithCategoryModel(Transaction? transaction) => new()
    {
        Id = transaction?.Id ?? "",
        Description = Reduce(transaction?.Description ?? ""),
        TransactionDate = transaction?.TransactionDate,
        Amount = transaction?.Payment?.Total ?? 0,
        Category = transaction?.Category?.Id ?? "",
        Subcategory = transaction?.Category?.Subcategory?.Id ?? "",
        UserId = transaction?.UserId ?? "",
    };
}

public class TotalModel
{
    public decimal Planned { get; set; } = default;
    public decimal Actual { get; set; } = default;
    public decimal Difference => Planned - Actual;
    public string Percentage => (Planned == 0 ? 0 : Actual / Planned * 100).ToString("P2");

    public void Calculate(IEnumerable<TotalModel> totals)
    {
        Planned = totals.Sum(x => x.Planned);
        Actual = totals.Sum(x => x.Actual);
    }

    public static explicit operator TotalModel(decimal? planned) =>
        new() { Planned = planned ?? 0, Actual = 0 };
}