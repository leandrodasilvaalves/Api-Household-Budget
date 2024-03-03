using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Entities;

namespace Household.Budget.Domain.Models;

public class BudgetTransactionWithCategoryModel : BudgetTransactionModel
{
    public BudgetTransactionWithCategoryModel() { }
    public BudgetTransactionWithCategoryModel(string category, string userId)
    {
        Category = category;
        UserId = userId;
    }

    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public string UserId { get; set; } = "";

    public (int?, Month) GetYearMonth() =>
        (TransactionDate?.Year, (Month)TransactionDate?.Month);

    public static explicit operator BudgetTransactionWithCategoryModel(Transaction transaction) => new()
    {
        Id = transaction?.Id,
        Description = Reduce(transaction?.Description),
        TransactionDate = transaction?.TransactionDate,
        Amount = transaction?.Payment?.Total ?? 0,
        Category = transaction?.Category?.Id,
        Subcategory = transaction?.Category?.Subcategory?.Id,
        UserId = transaction?.UserId,
    };
}