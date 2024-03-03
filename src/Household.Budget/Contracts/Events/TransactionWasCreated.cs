using Household.Budget.Domain.Entities;

namespace Household.Budget.Contracts.Events;

public class TransactionWasCreated : Event<Transaction>
{
    public TransactionWasCreated(Transaction data) =>
        Data = data ?? throw new ArgumentNullException(nameof(data));

    public override string Name => "TRANSACTION_WAS_CREATED";

    public override Transaction Data { get; }
}
