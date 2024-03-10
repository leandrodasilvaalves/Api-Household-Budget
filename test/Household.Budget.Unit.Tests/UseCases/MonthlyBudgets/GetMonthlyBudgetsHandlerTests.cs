using FluentAssertions;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets;

public class GetMonthlyBudgetsHandlerTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldReturnErrorwhenMonthlyBudgetDoesNotExistsAsync(GetMonthlyBudgetsHandler sut,
                                                                           GetMonthlyBudgetsRequest request,
                                                                           IMonthlyBudgetData monthlyBudgetData)
    {
        monthlyBudgetData.GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns((MonthlyBudget)null);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(MonthlyBudgetErrors.MONTHLY_BUDGET_NOT_FOUND);
        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldGetMonthlyBudgetByIdAsync(GetMonthlyBudgetsHandler sut,
                                                      GetMonthlyBudgetsRequest request,
                                                      IMonthlyBudgetData monthlyBudgetData)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await monthlyBudgetData.Received().GetOneAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
    }
}
