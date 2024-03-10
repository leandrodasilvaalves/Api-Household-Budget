using AutoFixture;

using Bogus;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;
using Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransactionNextPayment;
using Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;
using Household.Budget.UseCases.MonthlyBudgets.UpdateMonthlyBudget;

using MassTransit;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class MonthlyBudgetHandlersCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var categoryData = fixture.Create<ICategoryData>();
        CustomizeRequest(fixture, categoryData);
        fixture.Register(() => new CreateMonthlyBudgetHandler(fixture.Create<IMonthlyBudgetData>(), categoryData));
        fixture.Register(() => new GetMonthlyBudgetsHandler(fixture.Create<IMonthlyBudgetData>()));
        fixture.Register(() => new UpdateMonthlyBudgetHandler(fixture.Create<IMonthlyBudgetData>()));

        fixture.Register(() => Substitute.For<ICreateMonthlyBudgetHandler>());
        fixture.Register(() => Substitute.For<IBus>());
        fixture.Register(() => new AttachTransactionEventHandler(fixture.Create<IMonthlyBudgetData>(),
                                                                 fixture.Create<ICreateMonthlyBudgetHandler>(),
                                                                 fixture.Create<IBus>()));
        fixture.Register(() => new AttachTransactionNextPaymentEventHandler(fixture.Create<IMonthlyBudgetData>(),
                                                                 fixture.Create<ICreateMonthlyBudgetHandler>(),
                                                                 fixture.Create<IBus>()));
    }

    private static void CustomizeRequest(IFixture fixture, ICategoryData categoryData)
    {
        var request = fixture.Create<CreateMonthlyBudgetRequest>();
        var catoriesResult = fixture.Create<PagedListResult<Category>>();
        categoryData
            .GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(catoriesResult);

        request.Categories = MapToModel(catoriesResult.Items, request.Categories);
        fixture.Register(() => request);
    }

    private static List<BudgetCategoryRequestViewModel> MapToModel(List<Category> categories,
                                                                   List<BudgetCategoryRequestViewModel> requestCategories)
    {
        return (from c in categories
                select new BudgetCategoryRequestViewModel
                {
                    Id = c.Id,
                    Subcategories = (from s in c.Subcategories select new BudgetCategoryRequestViewModel { Id = s.Id }).ToList(),
                    PlannedTotal = requestCategories.Sum(c => c.Subcategories.Sum(s => s.PlannedTotal))
                }).ToList();
    }
}