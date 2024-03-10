using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Events;

using MassTransit;

namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler : IUpdateTransactionHandler
{
    private readonly ITransactionData _transactionData;
    private readonly ICategoryData _categoryData;
    private readonly ISubcategoryData _subcategoryData;
    private readonly IBus _bus;

    public UpdateTransactionHandler(ITransactionData transactionData, ICategoryData categoryData,
                                    ISubcategoryData subcategoryData, IBus bus)
    {
        _transactionData = transactionData ?? throw new ArgumentNullException(nameof(transactionData));
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
        _subcategoryData = subcategoryData ?? throw new ArgumentNullException(nameof(subcategoryData));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<UpdateTransactionResponse> HandleAsync(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        var transactionTask = _transactionData.GetByIdAsync(request.Id, request.UserId, cancellationToken);
        var categoryTask = _categoryData.GetByIdAsync(request?.Category?.Id, request?.UserId, cancellationToken);
        var subcategoryTask = _subcategoryData.GetByIdAsync(request?.Category?.Subcategory?.Id, request?.UserId, cancellationToken);

        await Task.WhenAll(transactionTask, categoryTask, subcategoryTask);

        var transaction = transactionTask.Result;
        var category = categoryTask.Result;
        var subcategory = subcategoryTask.Result;

        var contract = new UpdateTransactionRequestContract(request, transaction, category, subcategory);
        if (contract.IsValid is false)
        {
            return new UpdateTransactionResponse(contract.Notifications);
        }

        transaction.Merge(request, category, subcategory);
        await _transactionData.UpdateAsync(transaction, cancellationToken);
        await _bus.Publish(new TransactionWasUpdated(transaction), cancellationToken);
        return new UpdateTransactionResponse(transaction);
    }
}
