using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations.Data;

public class SubcategoryDataCustomizations : DataCustomizations<Subcategory, ISubcategoryData>

{
    public SubcategoryDataCustomizations() : base(() => Substitute.For<ISubcategoryData>()) { }
}