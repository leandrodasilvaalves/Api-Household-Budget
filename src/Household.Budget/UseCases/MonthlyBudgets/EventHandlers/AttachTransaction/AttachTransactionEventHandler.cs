using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Models;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

using MassTransit;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;

public class AttachTransactionEventHandler : IAttachTransactionEventHandler
{
    private readonly IMonthlyBudgetRepository _repository;
    private readonly ICreateMonthlyBudgetHandler _createMonthlyBudgetHandler;
    private readonly IBus _bus;

    public AttachTransactionEventHandler(IMonthlyBudgetRepository repository,
                                         ICreateMonthlyBudgetHandler createMonthlyBudgetHandler,
                                         IBus bus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _createMonthlyBudgetHandler = createMonthlyBudgetHandler ?? throw new ArgumentNullException(nameof(createMonthlyBudgetHandler));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<TransactionWasCreatedEventResponse> HandleAsync(TransactionWasCreated notification, CancellationToken cancellationToken)
    {
        var transaction = notification.Data;
        var (year, month) = GetYearMonth(transaction);
        var monthlyBudget = await _repository.GetOneAsync(transaction.UserId ?? "", year, month, cancellationToken);

        if (monthlyBudget is null)
        {
            lock (_repository)
            {
                CreateMonthlyBudgetRequest request = new() { Year = year, Month = month, UserId = transaction?.UserId ?? "" };
                _createMonthlyBudgetHandler.HandleAsync(request, cancellationToken).Wait(cancellationToken);
                _bus.Publish(notification, cancellationToken).Wait(cancellationToken);
                return new TransactionWasCreatedEventResponse(notification);
            }
        }

        monthlyBudget?.AttachTransaction(transaction);
        await _repository.UpdateAsync(monthlyBudget, cancellationToken);
        return new TransactionWasCreatedEventResponse(notification);
    }

    private static (int, Month) GetYearMonth(Transaction transaction)
    {
        var year = transaction.TransactionDate.Year;
        int? month = transaction.TransactionDate.Month;

        if (transaction.Payment.Type == PaymentType.CREDIT_CARD)
        {
            month = transaction.Payment?.CreditCard?.Installment?.NextPayments?.FirstOrDefault()?.DueDate.Month;
            year = transaction.TransactionDate.Year;
        }
        return (year, (Month)month);
    }
}