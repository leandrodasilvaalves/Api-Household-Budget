using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class TransactionWasUpdated : Event<Transaction>
{
    public TransactionWasUpdated(Transaction data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
        AddMetaData(data.GetMetaData());
    }

    public override string Name => "TRANSACTION_WAS_UPDATED";

    public override Transaction Data { get; }

    public bool TransactionDateHasChanged()
    {
        bool HasChanged(DateTime value) =>
            value.Month != Data.TransactionDate.Month ||
            value.Year != Data.TransactionDate.Year;

        var keyName = nameof(Transaction.TransactionDate);
        return
            MetaData.TryGetValue(keyName, out var transactionDate)
            && HasChanged(DateTime.Parse($"{transactionDate}"));
    }
}