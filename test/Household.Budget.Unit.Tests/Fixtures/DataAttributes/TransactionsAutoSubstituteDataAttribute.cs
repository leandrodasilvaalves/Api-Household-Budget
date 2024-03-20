using AutoFixture;
using AutoFixture.Xunit2;

using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Unit.Tests.Fixtures.Customizations;
using Household.Budget.Unit.Tests.Fixtures.Customizations.Data;
using Household.Budget.Unit.Tests.Fixtures.Fakers.Transactions;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.DataAttributes;

public class TransactionsAutoSubstituteDataAttribute : AutoDataAttribute
{
    public TransactionsAutoSubstituteDataAttribute() : base(() => Factory()) { }

    private static Fixture Factory()
    {
        var fixture = new Fixture();

        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        RegisterCategoryData(fixture);
        fixture.Customize(new TransactionDataCustomizations());
        fixture.Customize(new TransactionHandlersCustomizations());

        fixture.Register(() => new CreateTransactionRequestFaker(fixture));
        fixture.Register(() => new UpdateTransactionRequestFaker(fixture));
        return fixture;
    }

    public static void RegisterCategoryData(Fixture fixture)
    {
        var categoryData = Substitute.For<ICategoryData>();
        var subcategoryData = Substitute.For<ISubcategoryData>();
        var category = fixture.Create<Category>();

        categoryData
            .GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(category);

        var subcategory = category.Subcategories.FirstOrDefault();
        subcategoryData
            .GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(new Subcategory { Name = subcategory.Name, Id = subcategory.Id });

        fixture.Register(() => categoryData);
        fixture.Register(() => subcategoryData);
    }
}