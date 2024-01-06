using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionHandler : ICreateTransactionHandler
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubcategoryRepository _subcategoryRepository;

    public CreateTransactionHandler(ITransactionRepository transactionRepository,
                                    ICategoryRepository categoryRepository,
                                    ISubcategoryRepository subcategoryRepository)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _subcategoryRepository = subcategoryRepository ?? throw new ArgumentNullException(nameof(subcategoryRepository));
    }

    public async Task<CreateTransactionResponse> HandleAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var categoryTask = _categoryRepository.GetByIdAsync(request.Category.Id, request.UserId, cancellationToken);
        var subcategoryTask = _subcategoryRepository.GetByIdAsync(request.Category.Subcategory?.Id ?? "", request.UserId, cancellationToken);

        await Task.WhenAll(categoryTask, subcategoryTask);
        var category = categoryTask.Result;
        var subcategory = subcategoryTask.Result;

        var contract = new CreateTransactionRequestContract(request, category, subcategory);
        if(contract.IsValid is false)
        {
            return new CreateTransactionResponse(contract.Notifications);
        }

        var transaction = request.ToModel(category, subcategory);
        await _transactionRepository.CreateAsync(transaction, cancellationToken);
        return new CreateTransactionResponse(transaction);
    }
}