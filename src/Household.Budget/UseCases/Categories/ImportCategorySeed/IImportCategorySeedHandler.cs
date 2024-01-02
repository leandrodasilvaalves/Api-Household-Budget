namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public interface IImportCategorySeedHandler
{
    Task<ImportCategorySeedResponse> HandleAsync(ImportCategorySeedRequest request, CancellationToken cancellationToken);
}
