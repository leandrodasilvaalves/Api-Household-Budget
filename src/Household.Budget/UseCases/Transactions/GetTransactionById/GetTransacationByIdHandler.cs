using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Transactions.GetTransactionById;

public class GetTransacationByIdHandler : IGetTransacationByIdHandler
{
    private readonly ITransactionData _Data;

    public GetTransacationByIdHandler(ITransactionData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<GetTransacationByIdResponse> HandleAsync(GetTransacationByIdRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _Data.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return transaction is { } ?
            new GetTransacationByIdResponse(transaction) :
            new GetTransacationByIdResponse(TransactionErrors.TRANSACTION_NOT_FOUND);
    }
}

