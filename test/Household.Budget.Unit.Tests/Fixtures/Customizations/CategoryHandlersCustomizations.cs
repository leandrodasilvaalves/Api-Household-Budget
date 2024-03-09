using AutoFixture;

using Household.Budget.Domain.Data;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;
using Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;
using Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;
using Household.Budget.UseCases.Categories.GetCategoryById;
using Household.Budget.UseCases.Categories.ImportCategorySeed;
using Household.Budget.UseCases.Categories.ListCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

using MassTransit;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations;

public class CategoryHandlersCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new CreateCategoryHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new UpdateCategoryHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new GetCategoryByIdHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new ListCategoriesHandler(fixture.Create<ICategoryData>()));


        RegisterCreateCategoryHandler(fixture);
        fixture.Register(() => Substitute.For<ILogger<ImportCategorySeedHandler>>());
        fixture.Register(() => Substitute.For<IBus>());
        fixture.Register(() => new ImportCategorySeedHandler(fixture.Create<IBus>(),
                                                             fixture.Create<ILogger<ImportCategorySeedHandler>>(),
                                                             fixture.Create<ICreateCategoryHandler>()));

        fixture.Register(() => new AttachSubcategoryEventHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new DetachSubcategoryEventHandler(fixture.Create<ICategoryData>()));
        fixture.Register(() => new SubcategoryChangeCategoryEventHandler(fixture.Create<ICategoryData>()));
    }

    private static void RegisterCreateCategoryHandler(IFixture fixture)
    {
        var createCategoryHandler = Substitute.For<ICreateCategoryHandler>();

        createCategoryHandler
            .HandleAsync(Arg.Any<CreateCategoryRequest>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<CreateCategoryResponse>());

        fixture.Register(() => createCategoryHandler);
    }
}
