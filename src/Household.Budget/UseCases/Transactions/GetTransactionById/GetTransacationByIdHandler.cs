using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Transactions.GetTransactionById;

public class GetTransacationByIdHandler : IGetTransacationByIdHandler
{
    private readonly ITransactionRepository _repository;

    public GetTransacationByIdHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetTransacationByIdResponse> HandleAsync(GetTransacationByIdRequest request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync($"{request.Id}", request.UserId, cancellationToken);
        return transaction is { } ?
            new GetTransacationByIdResponse(transaction) :
            new GetTransacationByIdResponse(TransactionErrors.TRANSACTION_NOT_FOUND);
    }
}

