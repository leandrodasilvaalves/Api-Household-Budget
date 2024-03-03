using System.Runtime.CompilerServices;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

using MassTransit;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment;

public class AttachTransactionNextPaymentEventHandler : IAttachTransactionNextPaymentEventHandler
{
    private readonly IMonthlyBudgetRepository _repository;
    private readonly ICreateMonthlyBudgetHandler _createMonthlyBudgetHandler;
    private readonly IBus _bus;

    public AttachTransactionNextPaymentEventHandler(IMonthlyBudgetRepository repository,
                                         ICreateMonthlyBudgetHandler createMonthlyBudgetHandler,
                                         IBus bus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _createMonthlyBudgetHandler = createMonthlyBudgetHandler ?? throw new ArgumentNullException(nameof(createMonthlyBudgetHandler));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task HandleAsync(BudgetTransactionWithCategoryModel request, CancellationToken cancellationToken)
    {
        var (year, month) = request.GetYearMonth();
        var monthlyBudget = await _repository.GetOneAsync(request.UserId ?? "", year.Value, month, cancellationToken);

        if (monthlyBudget is null)
        {
            lock (_repository)
            {
                CreateMonthlyBudgetRequest monthlyBudgetRequest = new() { Year = year.Value, Month = month, UserId = request?.UserId ?? "" };
                _createMonthlyBudgetHandler.HandleAsync(monthlyBudgetRequest, cancellationToken).Wait(cancellationToken);
                _bus.Publish(request, cancellationToken).Wait(cancellationToken);
                return;
            }
        }

        monthlyBudget?.AttachTransaction(request);
        await _repository.UpdateAsync(monthlyBudget, cancellationToken);
    }
}