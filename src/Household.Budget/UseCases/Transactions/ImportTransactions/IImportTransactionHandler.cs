namespace Household.Budget.UseCases.Transactions.ImportTransactions;

public interface IImportTransactionHandler
{
    Task<ImportTransactionResponse> HandleAsync(ImportTransactionRequest request, CancellationToken cancellationToken);
}
