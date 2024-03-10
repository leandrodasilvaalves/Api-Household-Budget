using FluentAssertions;

using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

using NSubstitute;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets;
public class CreateMonthlyBudgetHandlerTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldReturnErrorWhenMonthlyBudgetAlreadyExistsAsync(CreateMonthlyBudgetHandler sut,
                                                                           CreateMonthlyBudgetRequest request,
                                                                           IMonthlyBudgetData monthlyBudgetData,
                                                                           ICategoryData categoryData)
    {
        monthlyBudgetData.ExistsAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(e => e.Key == BudgetError.BUGET_ALREADY_EXISTS.Key);
        await monthlyBudgetData.Received().ExistsAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.DidNotReceive().CreateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
        await categoryData.DidNotReceive().GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldReturnErrorWhenMonthlyBudgetRequestIsInvalidAsync(CreateMonthlyBudgetHandler sut,
                                                                             CreateMonthlyBudgetRequest request,
                                                                             IMonthlyBudgetData monthlyBudgetData,
                                                                             ICategoryData categoryData)
    {
        request.Categories.Add(new BudgetCategoryRequestViewModel { Id = "unknownId" });

        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCountGreaterThanOrEqualTo(1);
        await monthlyBudgetData.Received().ExistsAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await categoryData.Received().GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.DidNotReceive().CreateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public async Task ShouldCreateMonthlyBudgetAsync(CreateMonthlyBudgetHandler sut,
                                                     CreateMonthlyBudgetRequest request,
                                                     IMonthlyBudgetData monthlyBudgetData,
                                                     ICategoryData categoryData)
    {
        var result = await sut.HandleAsync(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await monthlyBudgetData.Received().ExistsAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<Month>(), Arg.Any<CancellationToken>());
        await categoryData.Received().GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await monthlyBudgetData.Received().CreateAsync(Arg.Any<MonthlyBudget>(), Arg.Any<CancellationToken>());
    }
}