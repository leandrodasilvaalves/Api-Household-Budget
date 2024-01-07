namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public interface IUpdateTransactionHandler
{
    Task<UpdateTransactionResponse> HandleAsync(UpdateTransactionRequest request, CancellationToken cancellationToken);
}
