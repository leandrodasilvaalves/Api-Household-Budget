using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Events;

public class TransactionWasUpdated : Event<Transaction>
{
    public TransactionWasUpdated(Transaction data) =>
        Data = data ?? throw new ArgumentNullException(nameof(data));

    public override string Name => "TRANSACTION_WAS_UPDATED";

    public override Transaction Data { get; }
}