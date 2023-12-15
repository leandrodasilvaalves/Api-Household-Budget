using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Repositories;

namespace Household.Budget;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(IRavenDbContext context)
        : base(context) { }
}
