using Household.Budget.Contracts.Data;
using Household.Budget.Infra.Data.Repositories;

namespace Household.Budget;

public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
{
    public SubcategoryRepository(IRavenDbContext context)
        : base(context) { }
}
