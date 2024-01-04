using Household.Budget.Contracts.Data;

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
        //CREDIT_CARD_IS_ONLY_ALLOWED_WHEN_CATEGORY_TYPE_IS_EXPENSES
        //SUBCATEGORY_MUST_BE_CHILD_OF_CATEGORY
        
        var transaction = request.ToModel();
        await _transactionRepository.CreateAsync(transaction, cancellationToken);
        return new CreateTransactionResponse(transaction); //TODO: ajustar aqui
    }
}