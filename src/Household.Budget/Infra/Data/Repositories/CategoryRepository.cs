using Household.Budget.Domain.Data;
using Household.Budget.Domain.Entities;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

namespace Household.Budget;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDbContext<Category> context)
        : base(context) { }
}
