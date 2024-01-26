using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;

public class AttachTransactionEventHandler : IAttachTransactionEventHandler
{
    private readonly IMonthlyBudgetRepository _repository;
    private readonly ICreateMonthlyBudgetHandler _createMonthlyBudgetHandler;

    public AttachTransactionEventHandler(IMonthlyBudgetRepository repository,
                                         ICreateMonthlyBudgetHandler createMonthlyBudgetHandler)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _createMonthlyBudgetHandler = createMonthlyBudgetHandler ?? throw new ArgumentNullException(nameof(createMonthlyBudgetHandler));
    }

    public async Task<TransactionWasCreatedEventResponse> HandleAsync(TransactionWasCreated notification, CancellationToken cancellationToken)
    {
        var transaction = notification.Data;
        var year = transaction.TransactionDate.Year;
        var month = (Month)transaction.TransactionDate.Month;
        var monthlyBudget = await _repository.GetOneAsync(transaction.UserId ?? "", year, month, cancellationToken);

        if (monthlyBudget is null)
        {
            CreateMonthlyBudgetRequest request = new() { Year = year, Month = month, UserId = transaction?.UserId ?? "" };
            var response = await _createMonthlyBudgetHandler.HandleAsync(request, cancellationToken);
            if (response.IsSuccess is false)
            {
                if (response.Errors.Any(x => x.Equals(BudgetError.BUGET_ALREADY_EXISTS)))
                {
                    return await HandleAsync(notification, cancellationToken);
                }
                return new TransactionWasCreatedEventResponse(response?.Errors ?? []);
            }
            monthlyBudget = response.Data;
        }

        monthlyBudget?.AttachTransaction(transaction);
        await _repository.UpdateAsync(monthlyBudget, cancellationToken);
        return new TransactionWasCreatedEventResponse(notification);
    }
}