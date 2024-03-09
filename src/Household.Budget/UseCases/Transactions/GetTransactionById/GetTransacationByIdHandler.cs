using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Transactions.GetTransactionById;

public class GetTransacationByIdHandler : IGetTransacationByIdHandler
{
    private readonly ITransactionData _data;

    public GetTransacationByIdHandler(ITransactionData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<GetTransacationByIdResponse> HandleAsync(GetTransacationByIdRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _data.GetByIdAsync(request.Id, request.UserId, cancellationToken);
        return transaction is { } ?
            new GetTransacationByIdResponse(transaction) :
            new GetTransacationByIdResponse(TransactionErrors.TRANSACTION_NOT_FOUND);
    }
}

