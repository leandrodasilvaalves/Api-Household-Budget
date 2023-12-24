using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

using Raven.Client.Documents;

namespace Household.Budget.Infra.Data.Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
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

    public Task<List<T>> GetAllAsync(int pageSize, int pageNumber, string userId, CancellationToken cancellationToken = default)
    {
        int skip = pageSize * (pageNumber - 1);
        int take = pageSize;

        var session = _context.Store.OpenAsyncSession();
        return session.Query<T>().Skip(skip).Take(take)
            .Where(x => x.Status == ModelStatus.ACTIVE &&
                  (x.Owner == ModelOwner.SYSTEM || x.UserId == userId))
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
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