using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Data;

public interface ICategoryRepository : IRepository<Category>
{
    Task<PagedListResult<Category>> GetAllAsync(int pageSize,
                                                int pageNumber,
                                                string userId,
                                                CancellationToken cancellationToken,
                                                params string[] Ids);
}