using System.Linq.Expressions;

using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets;

public class UpdateMonthlyBudgetHandlerTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldReturnErrorWhenRequestIsInvalid(UpdateMonthlyBudgetHandler sut,
                                                            UpdateMonthlyBudgetRequest request,
                                                            IMonthlyBudgetData monthlyBudgetData)
    {
        monthlyBudgetData.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns((MonthlyBudget)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCountGreaterThanOrEqualTo(1);
        await monthlyBudgetData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<Expression<Func<MonthlyBudget, bool>>>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.DidNotReceive().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldUpdateMonthlyBudgetAsync(UpdateMonthlyBudgetHandler sut,
                                                     UpdateMonthlyBudgetRequest request,
                                                     IMonthlyBudgetData monthlyBudgetData)
    {
        monthlyBudgetData.GetOneAsync(Arg.Any<Expression<Func<MonthlyBudget, bool>>>(), Arg.Any<CancellationToken>())
           .Returns((MonthlyBudget)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await monthlyBudgetData.Received().GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<Expression<Func<MonthlyBudget, bool>>>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().UpdateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }
}