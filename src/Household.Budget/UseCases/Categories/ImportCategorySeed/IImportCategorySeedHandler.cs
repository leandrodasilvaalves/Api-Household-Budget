namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public interface IImportCategorySeedHandler
{
    Task<ImportCategorySeedResponse> Handle(ImportCategorySeedRequest request, CancellationToken cancellationToken);
}
