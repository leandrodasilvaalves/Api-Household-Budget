using Household.Budget.Domain.Data;
using Household.Budget.Domain.Models;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

using MassTransit;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment;

public class AttachTransactionNextPaymentEventHandler : IAttachTransactionNextPaymentEventHandler
{
    private readonly IMonthlyBudgetData _Data;
    private readonly ICreateMonthlyBudgetHandler _createMonthlyBudgetHandler;
    private readonly IBus _bus;

    public AttachTransactionNextPaymentEventHandler(IMonthlyBudgetData Data,
                                         ICreateMonthlyBudgetHandler createMonthlyBudgetHandler,
                                         IBus bus)
    {
        _Data = Data ?? throw new ArgumentNullException(nameof(Data));
        _createMonthlyBudgetHandler = createMonthlyBudgetHandler ?? throw new ArgumentNullException(nameof(createMonthlyBudgetHandler));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task HandleAsync(BudgetTransactionWithCategoryModel request, CancellationToken cancellationToken)
    {
        var (year, month) = request.GetYearMonth();
        var monthlyBudget = await _Data.GetOneAsync(request.UserId ?? "", year.Value, month, cancellationToken);

        if (monthlyBudget is null)
        {
            lock (_Data)
            {
                CreateMonthlyBudgetRequest monthlyBudgetRequest = new() { Year = year.Value, Month = month, UserId = request?.UserId ?? "" };
                _createMonthlyBudgetHandler.HandleAsync(monthlyBudgetRequest, cancellationToken).Wait(cancellationToken);
                _bus.Publish(request, cancellationToken).Wait(cancellationToken);
                return;
            }
        }

        monthlyBudget?.AttachTransaction(request);
        await _Data.UpdateAsync(monthlyBudget, cancellationToken);
    }
}