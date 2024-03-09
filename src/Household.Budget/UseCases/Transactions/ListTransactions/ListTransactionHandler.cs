using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionHandler : IListTransactionHandler
{
    private readonly ITransactionData _data;

    public ListTransactionHandler(ITransactionData data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<ListTransactionResponse> HandleAsync(ListTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await _data.GetAllAsync(request.Year, request.Month, request.PageSize,
                                                   request.PageNumber, request.UserId, cancellationToken);
        return new ListTransactionResponse(result);
    }
}
