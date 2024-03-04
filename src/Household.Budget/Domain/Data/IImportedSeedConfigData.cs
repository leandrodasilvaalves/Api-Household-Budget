using Household.Budget.Domain.Entities;

namespace Household.Budget.Domain.Data
{
    public interface IImportedSeedConfigData
    {
        Task<ImportedSeedConfig> GetAsync(CancellationToken cancellationToken = default);
        Task SaveAsync(ImportedSeedConfig seedConfig, CancellationToken cancellationToken = default);
        Task<bool> RootUserHasBeenCreatedAsync(CancellationToken cancellationToken = default);
        Task<bool> CategoriesHasBeenImportedAsync(CancellationToken cancellationToken = default);
    }
}