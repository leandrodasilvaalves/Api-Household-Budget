using Household.Budget.Contracts.Entities;

using MongoDB.Driver;

namespace Household.Budget.Infra.Data.Context;

public interface IMongoDbContext<T> where T : Entity
{
    IMongoCollection<T> Collection { get; }
}
