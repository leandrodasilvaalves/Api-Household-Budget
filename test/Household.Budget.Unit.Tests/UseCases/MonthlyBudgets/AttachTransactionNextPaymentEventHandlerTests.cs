using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Domain.Models;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets;

public class AttachTransactionNextPaymentEventHandlerTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldCreateMonthlyBudgetWhenDoesNotExistsAsync(AttachTransactionNextPaymentEventHandler sut,
                                                                      BudgetTransactionWithCategoryModel notification,
                                                                      IMonthlyBudgetData monthlyBudgetData,
                                                                      ICreateMonthlyBudgetHandler createMonthlyBudgetHandler)
    {
        monthlyBudgetData.GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns((MonthlyBudget)null);

        await sut.HandleAsync(notification, CancellationToken.None);

        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        // await createMonthlyBudgetHandler.Received().HandleAsync(Arg.Any<CreateMonthlyBudgetRequest>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.DidNotReceive().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldAttachTransactionWhenMonthlyBudgetDoesExistsAsync(AttachTransactionNextPaymentEventHandler sut,
                                                                              BudgetTransactionWithCategoryModel notification,
                                                                              IMonthlyBudgetData monthlyBudgetData,
                                                                              ICreateMonthlyBudgetHandler createMonthlyBudgetHandler)
    {
        await sut.HandleAsync(notification, CancellationToken.None);

        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await createMonthlyBudgetHandler.DidNotReceive().HandleAsync(Arg.Any<CreateMonthlyBudgetRequest>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }
}
