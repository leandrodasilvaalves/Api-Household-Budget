namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public interface ICreateTransactionHandler
{
    Task<CreateTransactionResponse> HandleAsync(CreateTransactionRequest request, CancellationToken cancellationToken);
}
