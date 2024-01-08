using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Context;

using MongoDB.Driver;

namespace Household.Budget.Infra.Data.Repositories
{
    public class ImportedSeedConfigRespository : IImportedSeedConfigRespository
    {
        private readonly IMongoDbContext<ImportedSeedConfig> _context;

        public ImportedSeedConfigRespository(IMongoDbContext<ImportedSeedConfig> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ImportedSeedConfig> GetAsync(CancellationToken cancellationToken = default)
        {
            return _context.Collection.Find(x => true).FirstOrDefaultAsync(cancellationToken);
        }

        public Task SaveAsync(ImportedSeedConfig seedConfig, CancellationToken cancellationToken = default)
        {
            var options = new FindOneAndReplaceOptions<ImportedSeedConfig, ImportedSeedConfig> { IsUpsert = true };
            var filter = Builders<ImportedSeedConfig>.Filter.Eq(config => config.Id, seedConfig.Id);
            return _context.Collection.FindOneAndReplaceAsync(filter, seedConfig, options, cancellationToken);
        }

        public async Task<bool> RootUserHasBeenCreatedAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Collection.CountDocumentsAsync(x => x.RootUserHasBeenCreated == true, new CountOptions(), cancellationToken) == 1;
        }

        public async Task<bool> CategoriesHasBeenImportedAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Collection.CountDocumentsAsync(x => x.CategoriesHasBeenImported == true, new CountOptions(), cancellationToken) == 1;
        }
    }
}