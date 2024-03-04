using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data;

namespace Household.Budget;

public class SubcategoryData : Data<Subcategory>, ISubcategoryData
{
    public SubcategoryData(IMongoDbContext<Subcategory> context)
        : base(context) { }
}
