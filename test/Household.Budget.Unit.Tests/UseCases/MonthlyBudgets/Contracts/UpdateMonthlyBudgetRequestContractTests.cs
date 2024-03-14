using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.MonthlyBudgets;
using Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets.Contracts;

public class UpdateMonthlyBudgetRequestContractTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenCategoryIdIsNull(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithCategoryIdNull());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenCategoryIdIsEmpty(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithCategoryIdEmpty());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenSubcategoryIdIsNull(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithSubcategoryIdNull());
        result.Notifications.Should().Contain(c => c.Key == SubcategoryErrors.SUBCATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenSubcategoryIdIsEmpty(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithSubcategoryIdEmpty());
        result.Notifications.Should().Contain(c => c.Key == SubcategoryErrors.SUBCATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenBudgetYearIsInformedAndYearIsInvalid(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithInvalidYear());
        result.Notifications.Should().Contain(c => c.Key == CommonErrors.INVALID_YEAR.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldNotFailWhenBudgetYearIsNotInformed(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithNullYear());
        result.Notifications.Should().NotContain(c => c.Key == CommonErrors.INVALID_YEAR.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenPlannedTotalIsInvalid(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(request.WithCategoryInvalidPlannedTotal());
        result.Notifications.Should().Contain(c => c.Key == BudgetError.CATEGORY_PLANNED_TOTAL_MUST_BE_SUM_OF_SUBCATEGORIES.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenMonthlyBudgetDoesNotExists(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(null, false, request);
        result.Notifications.Should().Contain(c => c.Key == BudgetError.BUGET_NOT_FOUND.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenTargetMonthlyBudgetAlreadyExists(UpdateMonthlyBudgetRequestFaker request)
    {
        var result = new UpdateMonthlyBudgetRequestContract(new(), true, request);
        result.Notifications.Should().Contain(c => c.Key == BudgetError.BUGET_ALREADY_EXISTS.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenNewPlannedTotalIsLessThanActualTotal(UpdateMonthlyBudgetRequestFaker request, MonthlyBudget monthlyBudget)
    {
        var result = new UpdateMonthlyBudgetRequestContract(monthlyBudget, false, request.WithCategoryInvalidPlannedTotal(monthlyBudget));
        result.Notifications.Should().Contain(c => c.Key == BudgetError.NEW_PLANNED_TOTAL_MUST_BE_GREATER_OR_EQUAL_TO_ACTUAL_TOTAL.Key);
    }
}