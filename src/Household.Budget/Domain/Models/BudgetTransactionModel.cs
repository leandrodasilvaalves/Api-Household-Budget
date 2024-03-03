using Household.Budget.Domain.Entities;

namespace Household.Budget.Domain.Models;

public class BudgetTransactionModel
{
    public string Id { get; set; } = "";
    public DateTime? TransactionDate { get; set; }
    public string? Description { get; set; } = "";
    public decimal Amount { get; set; }

    public static explicit operator BudgetTransactionModel(Transaction transaction) => new()
    {
        Id = transaction?.Id,
        Description = Reduce(transaction?.Description),
        TransactionDate = transaction?.TransactionDate,
        Amount = transaction?.Payment?.Total ?? 0
    };

    protected static string Reduce(string value, short maxLength = 30) =>
        value.Length > maxLength ? value[..maxLength] : value;
}
