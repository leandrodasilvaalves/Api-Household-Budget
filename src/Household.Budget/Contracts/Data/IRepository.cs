using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Data;

public interface IRepository<T> where T : BaseModel
{
    Task CreateAsync(T model, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(int pageSize, int pageNumber, string userId, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(string id, string userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(T model, CancellationToken cancellationToken = default);
}
