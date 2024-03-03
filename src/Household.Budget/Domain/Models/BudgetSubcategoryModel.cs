using Household.Budget.Domain.Entities;

namespace Household.Budget.Domain.Models;

public class BudgetSubcategoryModel : BudgetCategoryModel
{
    public List<BudgetTransactionModel> Transactions { get; set; }
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

    public void UpdateTransaction(Transaction transaction)
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
