using System.Linq.Expressions;

using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

namespace Household.Budget;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDbContext<Category> context)
        : base(context) { }

    public Task<PagedListResult<Category>> GetAllAsync(int pageSize,
                                                       int pageNumber,
                                                       string userId,
                                                       CancellationToken cancellationToken,
                                                       params string[] Ids)
    {
        Expression<Func<Category, bool>>? predicate = x => Ids.Contains(x.Id);
        return GetAllAsync(pageSize, pageNumber, userId, predicate, cancellationToken);
    }
}
