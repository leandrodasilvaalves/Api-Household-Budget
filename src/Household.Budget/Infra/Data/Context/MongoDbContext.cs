using Household.Budget.Contracts.Models;

using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Household.Budget.Infra.Data.Context;

public class MongoDbContext<T> : IMongoDbContext<T> where T : Model
{
    public MongoDbContext(IOptionsMonitor<MongoConfig> options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        LoadCustomConfiguration();
        Collection = ConfigureCollection(options.CurrentValue);
    }

    public IMongoCollection<T> Collection { get; }

    private static void LoadCustomConfiguration()
    {
        var conventionPack = new ConventionPack {
            new CamelCaseElementNameConvention(),
            new EnumRepresentationConvention(BsonType.String),
            new IgnoreIfNullConvention(true),
        };

        ConventionRegistry.Register("camelCase", conventionPack, t => true);
        ConventionRegistry.Register("EnumStringConvention", conventionPack, t => true);
        ConventionRegistry.Register("IgnoreIfNull", conventionPack, t => true);
    }

    private IMongoCollection<T> ConfigureCollection(MongoConfig options)
    {
        var mongoClient = new MongoClient(options.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.DatabaseName);
        return mongoDatabase.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}
