using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations.Data;


public class CategoryDataCustomizations : DataCustomizations<Category, ICategoryData>
{
    public CategoryDataCustomizations() : base(() => Substitute.For<ICategoryData>()) { }
}