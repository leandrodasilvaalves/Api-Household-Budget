using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Household.Budget.Infra.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Model
{
    private readonly IRavenDbContext _context;

    public Repository(IRavenDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateAsync(T model, CancellationToken cancellationToken = default)
    {
        var session = _context.Store.OpenAsyncSession();
        await session.StoreAsync(model, cancellationToken);
        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedListResult<T>> GetAllAsync(int pageSize, int pageNumber, string userId, CancellationToken cancellationToken = default)
    {
        int skip = pageSize * (pageNumber - 1);
        int take = pageSize;

        var session = _context.Store.OpenAsyncSession();
        var items = await session.Query<T>()
            .Statistics(out QueryStatistics statistics)
            .Skip(skip).Take(take)
            .Where(x => x.Status == ModelStatus.ACTIVE &&
                  (x.Owner == ModelOwner.SYSTEM || x.UserId == userId))
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);

        return new PagedListResult<T>(items, statistics.TotalResults, pageSize, pageNumber);
    }

    public Task<T> GetByIdAsync(string id, string userId, CancellationToken cancellationToken = default)
    {
        var session = _context.Store.OpenAsyncSession();
        return session.Query<T>()
            .Where(x => x.Id == id && x.Status == ModelStatus.ACTIVE &&
                  (x.Owner == ModelOwner.SYSTEM || x.UserId == userId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(T model, CancellationToken cancellationToken = default)
    {
        var session = _context.Store.OpenAsyncSession();
        await session.StoreAsync(model, cancellationToken);
        await session.SaveChangesAsync(cancellationToken);
    }
}