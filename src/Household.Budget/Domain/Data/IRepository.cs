using System.Linq.Expressions;

using Household.Budget.Contracts.Entities;

namespace Household.Budget.Domain.Data;

public interface IRepository<T> where T : Entity
{
    Task CreateAsync(T model, CancellationToken cancellationToken = default);
    Task<PagedListResult<T>> GetAllAsync(int pageSize, int pageNumber, string userId, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(string id, string userId, CancellationToken cancellationToken = default);
    Task<T> GetOneAsync(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default);
    Task UpdateAsync(T model, CancellationToken cancellationToken = default);
}
