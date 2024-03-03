using Household.Budget.Domain.Data;

namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionHandler : IListTransactionHandler
{
    private readonly ITransactionRepository _repository;

    public ListTransactionHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ListTransactionResponse> HandleAsync(ListTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(request.Year, request.Month, request.PageSize,
                                                   request.PageNumber, request.UserId, cancellationToken);
        return new ListTransactionResponse(result);
    }
}
