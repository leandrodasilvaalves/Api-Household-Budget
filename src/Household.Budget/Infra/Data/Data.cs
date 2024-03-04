using System.Linq.Expressions;

using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Entities;
using Household.Budget.Infra.Data.Context;

using MongoDB.Driver;

namespace Household.Budget.Infra.Data;

public class Data<T> : IData<T> where T : Entity
{
    private readonly IMongoDbContext<T> _context;

    public Data(IMongoDbContext<T> context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<PagedListResult<T>> GetAllAsync(int pageSize, int pageNumber, string userId,
                                                CancellationToken cancellationToken = default)
    {

        Expression<Func<T, bool>> predicate = x => x.Status == ModelStatus.ACTIVE &&
                           (x.Owner == ModelOwner.SYSTEM || x.UserId == userId);

        return GetAllAsync(pageSize, pageNumber, userId, predicate, cancellationToken);
    }

    protected async Task<PagedListResult<T>> GetAllAsync(int pageSize,
                                                         int pageNumber,
                                                         string userId,
                                                         Expression<Func<T, bool>>? predicate = null,
                                                         CancellationToken cancellationToken = default)
    {
        int skip = pageSize * (pageNumber - 1);

        predicate ??= x => x.Status == ModelStatus.ACTIVE &&
                           (x.Owner == ModelOwner.SYSTEM || x.UserId == userId);

        var count = _context.Collection
            .CountDocumentsAsync(predicate, new CountOptions(), cancellationToken);

        var items = Task.Run(() => _context.Collection
            .AsQueryable()
            .Where(predicate)
            .Skip(skip)
            .Take(pageSize)
            .OrderByDescending(x => x.UpdatedAt)
            .ToList(), cancellationToken);

        await Task.WhenAll(count, items);
        return new PagedListResult<T>(items.Result, count.Result, pageSize, pageNumber);
    }

    public Task<T> GetByIdAsync(string id, string userId, CancellationToken cancellationToken = default) =>
        GetOneAsync(x => x.Id == id && (x.Owner == ModelOwner.SYSTEM || x.UserId == userId), cancellationToken);

    public Task<T> GetOneAsync(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken) =>
        _context.Collection.Find(predicate).FirstOrDefaultAsync(cancellationToken);

    public Task CreateAsync(T model, CancellationToken cancellationToken = default) =>
        _context.Collection.InsertOneAsync(model, new InsertOneOptions(), cancellationToken);

    public Task UpdateAsync(T model, CancellationToken cancellationToken = default) =>
        _context.Collection.ReplaceOneAsync(x => x.Id == model.Id, model, new ReplaceOptions(), cancellationToken);
}