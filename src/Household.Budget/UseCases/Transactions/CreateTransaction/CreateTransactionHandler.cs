using Household.Budget.Contracts.Data;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionHandler : ICreateTransactionHandler
{
    private readonly ITransactionRepository _repository;

    public CreateTransactionHandler(ITransactionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<CreateTransactionResponse> HandleAsync(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var transaction = request.ToModel();
        await _repository.CreateAsync(transaction, cancellationToken);
        return new CreateTransactionResponse(transaction); //TODO: ajustar aqui
    }
}