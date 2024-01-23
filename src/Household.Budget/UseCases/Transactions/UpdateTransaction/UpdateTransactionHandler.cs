using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Events;

using MassTransit;

namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionHandler : IUpdateTransactionHandler
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IBus _bus;

    public UpdateTransactionHandler(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository,
                                    ISubcategoryRepository subcategoryRepository, IBus bus)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<UpdateTransactionResponse> HandleAsync(UpdateTransactionRequest request, CancellationToken cancellationToken)
    {
        var transactionTask = _transactionRepository.GetByIdAsync(request.Id, request.UserId, cancellationToken);
        var categoryTask = _categoryRepository.GetByIdAsync(request?.Category?.Id ?? "", request?.UserId ?? "", cancellationToken);
        var subcategoryTask = _subcategoryRepository.GetByIdAsync(request?.Category?.Subcategory?.Id ?? "", request?.UserId ?? "", cancellationToken);

        await Task.WhenAll(transactionTask, categoryTask, subcategoryTask);

        var transaction = transactionTask.Result;
        var category = categoryTask.Result;
        var subcategory = subcategoryTask.Result;

        var contract = new UpdateTransactionRequestContract(request ?? new(), transaction, category, subcategory);
        if (contract.IsValid is false)
        {
            return new UpdateTransactionResponse(contract.Notifications);
        }

        transaction.Merge(request, category, subcategory);
        await _transactionRepository.UpdateAsync(transaction, cancellationToken);
        await _bus.Publish(new TransactionWasUpdated(transaction, transaction.GetMetaData()), cancellationToken);
        return new UpdateTransactionResponse(transaction);
    }
}
