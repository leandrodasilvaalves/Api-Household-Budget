namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public interface IImportCategorySeedHandler
{
    Task Handle(ImportCategorySeedRequest request, CancellationToken cancellationToken);
}
