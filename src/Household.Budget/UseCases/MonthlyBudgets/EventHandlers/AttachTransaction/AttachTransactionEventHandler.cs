using Household.Budget.Contracts.Data;
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
        var (year, month) = transaction.GetYearMonth();
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

        if (transaction.HasNextPayments())
        {
            var payment = transaction.GetFirstNextPayment();
            var model = (BudgetTransactionWithCategoryModel)transaction;
            model.Amount = payment.Amount;
            model.TransactionDate = payment.DueDate;
            monthlyBudget.AttachTransaction(model);
        }
        else
        {
            monthlyBudget?.AttachTransaction(transaction);
        }

        await Task.WhenAll(
            _repository.UpdateAsync(monthlyBudget, cancellationToken),
            PublishNextPaymentsWhenCreditCardAsync(transaction, cancellationToken)
        );

        return new TransactionWasCreatedEventResponse(notification);
    }

    private async Task PublishNextPaymentsWhenCreditCardAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        var budgetTransactionNextPayments = new List<BudgetTransactionWithCategoryModel>();
        var nextPayments = transaction.GetNextPayments(true);
        if (nextPayments.Count > 0)
        {
            foreach (var payment in nextPayments)
            {
                var model = (BudgetTransactionWithCategoryModel)transaction;
                model.Amount = payment.Amount;
                model.TransactionDate = payment.DueDate;
                budgetTransactionNextPayments.Add(model);
            }
            var endpoint = await _bus.GetPublishSendEndpoint<BudgetTransactionWithCategoryModel>();
            await endpoint.SendBatch(budgetTransactionNextPayments, cancellationToken);
        }
    }
}