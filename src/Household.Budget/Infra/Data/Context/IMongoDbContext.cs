using Household.Budget.Contracts.Models;

using MongoDB.Driver;

namespace Household.Budget.Infra.Data.Context;

public interface IMongoDbContext<T> where T : Model
{
    IMongoCollection<T> Collection { get; }
}
