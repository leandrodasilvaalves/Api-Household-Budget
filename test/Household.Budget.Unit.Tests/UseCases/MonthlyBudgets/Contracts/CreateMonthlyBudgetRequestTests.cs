using FluentAssertions;

using Household.Budget.Contracts.Errors;
using Household.Budget.Domain.Entities;
using Household.Budget.Domain.Models;
using Household.Budget.Unit.Tests.Fixtures.DataAttributes;
using Household.Budget.Unit.Tests.Fixtures.Fakers.MonthlyBudgets;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;

namespace Household.Budget.Unit.Tests.UseCases.MonthlyBudgets.Contracts;

public class CreateMonthlyBudgetRequestTests
{
    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenCategoryIdIsNull(CreateMonthlyBudgetRequestFaker request)
    {
        var result = new CreateMonthlyBudgetRequestContract(request.WithCategoryIdNull());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenCategoryIdIsEmpty(CreateMonthlyBudgetRequestFaker request)
    {
        var result = new CreateMonthlyBudgetRequestContract(request.WithCategoryIdEmpty());
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenSubcategoryIdIsNull(CreateMonthlyBudgetRequestFaker request)
    {
        var result = new CreateMonthlyBudgetRequestContract(request.WithSubcategoryIdNull());
        result.Notifications.Should().Contain(c => c.Key == SubcategoryErrors.SUBCATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenSubcategoryIdIsEmpty(CreateMonthlyBudgetRequestFaker request)
    {
        var result = new CreateMonthlyBudgetRequestContract(request.WithSubcategoryIdEmpty());
        result.Notifications.Should().Contain(c => c.Key == SubcategoryErrors.SUBCATEGORY_ID_IS_REQUIRED.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenCategoryDoesNotExists(CreateMonthlyBudgetRequestFaker request, List<Category> categories)
    {
        var result = new CreateMonthlyBudgetRequestContract(request, categories);
        result.Notifications.Should().Contain(c => c.Key == CategoryErrors.CATEGORY_NOT_FOUND.Key);
    }

    [Theory]
    [MonthlyBudgetAutoSubstituteData]
    public void ShouldFailWhenSubcategoryDoesNotExists(CreateMonthlyBudgetRequestFaker request, List<SubcategoryModel> subcategories)
    {
        var categories = from category in request.Categories
                         select new Category { Id = category.Id, Subcategories = subcategories };

        var result = new CreateMonthlyBudgetRequestContract(request, categories.ToList());
        result.Notifications.Should().Contain(c => c.Key == SubcategoryErrors.SUBCATEGORY_NOT_FOUND.Key);
    }
}