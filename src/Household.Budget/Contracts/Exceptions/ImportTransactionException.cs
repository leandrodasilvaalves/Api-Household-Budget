using Household.Budget.UseCases.Transactions.CreateTransaction;

namespace Household.Budget.Contracts.Exceptions;

public class ImportTransactionException : AbstractConsumersExceptions<CreateTransactionResponse>
{    public ImportTransactionException(CreateTransactionResponse response) : base(response) { }
}
