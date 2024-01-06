namespace Household.Budget.UseCases.Transactions.ListTransactions;

public interface IListTransactionHandler
{
    Task<ListTransactionResponse> HandleAsync(ListTransactionRequest request, CancellationToken cancellationToken);
}
