using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionHandler : IListTransactionHandler
{
    private readonly ITransactionData _Data;

    public ListTransactionHandler(ITransactionData Data)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
    }

    public async Task<ListTransactionResponse> HandleAsync(ListTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await _Data.GetAllAsync(request.Year, request.Month, request.PageSize,
                                                   request.PageNumber, request.UserId, cancellationToken);
        return new ListTransactionResponse(result);
    }
}
