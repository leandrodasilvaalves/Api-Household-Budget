using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Entities;

using MassTransit;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionHandler : ICreateTransactionHandler
{
    private readonly ITransactionData _transactionData;
    private readonly ICategoryData _categoryData;
    private readonly ISubcategoryData _subcategoryData;
    private readonly IBus _bus;

    public CreateTransactionHandler(ITransactionData transactionData,
                                    ICategoryData categoryData,
                                    ISubcategoryData subcategoryData,
                                    IBus bus)
    {
        _transactionData = transactionData ?? throw new ArgumentNullException(nameof(transactionData));
        _categoryData = categoryData ?? throw new ArgumentNullException(nameof(categoryData));
        _subcategoryData = subcategoryData ?? throw new ArgumentNullException(nameof(subcategoryData));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<CreateTransactionResponse> HandleAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var categoryTask = _categoryData.GetByIdAsync(request.Category.Id, request.UserId, cancellationToken);
        var subcategoryTask = _subcategoryData.GetByIdAsync(request.Category.Subcategory?.Id, request.UserId, cancellationToken);

        await Task.WhenAll(categoryTask, subcategoryTask);
        var category = categoryTask.Result;
        var subcategory = subcategoryTask.Result;

        var contract = new CreateTransactionRequestContract(request, category, subcategory);
        if (contract.IsValid is false)
        {
            return new CreateTransactionResponse(contract.Notifications);
        }

        var transaction = new Transaction();
        transaction.Create(request, category, subcategory);
        await _transactionData.CreateAsync(transaction, cancellationToken);
        await _bus.Publish(new TransactionWasCreated(transaction), cancellationToken);
        return new CreateTransactionResponse(transaction);
    }
}