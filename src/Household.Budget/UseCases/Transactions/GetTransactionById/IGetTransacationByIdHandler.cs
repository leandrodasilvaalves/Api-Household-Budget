namespace Household.Budget.UseCases.Transactions.GetTransactionById;

public interface IGetTransacationByIdHandler
{
    Task<GetTransacationByIdResponse> HandleAsync(GetTransacationByIdRequest request, CancellationToken cancellationToken);
}

