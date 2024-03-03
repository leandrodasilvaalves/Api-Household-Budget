using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

namespace Household.Budget;

public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
{
    public SubcategoryRepository(IMongoDbContext<Subcategory> context)
        : base(context) { }
}
