using Household.Budget.Contracts.Data;
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

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var session =_context.Store.OpenAsyncSession();
        return session.Query<T>().ToListAsync(cancellationToken);
    }

    public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var session = _context.Store.OpenAsyncSession();
        return session.Query<T>().Where(x => x.Id == $"{id}").FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(T model, CancellationToken cancellationToken = default)
    {
        var session = _context.Store.OpenAsyncSession();
        await session.StoreAsync(model, cancellationToken);
        await session.SaveChangesAsync(cancellationToken);
    }
}