using FluentAssertions;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Events;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets;

public class AttachTransactionEventHandlerTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldCreateMonthlyBudgetWhenDoesNotExistsAsync(AttachTransactionEventHandler sut,
                                                                      TransactionWasCreated notification,
                                                                      IMonthlyBudgetData monthlyBudgetData,
                                                                      ICreateMonthlyBudgetHandler createMonthlyBudgetHandler)
    {
        monthlyBudgetData.GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns((MonthlyBudget)null);

        var result = await sut.HandleAsync(notification, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        // await createMonthlyBudgetHandler.Received().HandleAsync(Arg.Any<CreateMonthlyBudgetRequest>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.DidNotReceive().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldAttachTransactionWhenMonthlyBudgetDoesExistsAsync(AttachTransactionEventHandler sut,
                                                                              TransactionWasCreated notification,
                                                                              IMonthlyBudgetData monthlyBudgetData,
                                                                              ICreateMonthlyBudgetHandler createMonthlyBudgetHandler)
    {
        var result = await sut.HandleAsync(notification, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await createMonthlyBudgetHandler.DidNotReceive().HandleAsync(Arg.Any<CreateMonthlyBudgetRequest>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }
}