using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Domain.Models;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.DetachTransaction;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets;

public class DetachTransactionEventHandlerTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldNotDetatchTransactionWhenMonthlyBudgetDoesNotExistsAsync(DetachTransactionEventHandler sut,
                                                                                     TransactionWasUpdated notification,
                                                                                     IMonthlyBudgetData monthlyBudgetData)
    {
        monthlyBudgetData.GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns((MonthlyBudget)null);

        await sut.HandleAsync(notification, CancellationToken.None);

        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.DidNotReceive().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldNotDetatchTransactionAsync(AttachTransactionNextPaymentEventHandler sut,
                                                       BudgetTransactionWithCategoryModel notification,
                                                       IMonthlyBudgetData monthlyBudgetData)
    {
        await sut.HandleAsync(notification, CancellationToken.None);

        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }
}

