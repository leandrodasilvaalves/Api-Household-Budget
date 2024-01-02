using Household.Budget.UseCases.Categories.ImportCategorySeed;

namespace Household.Budget.Contracts.Exceptions;

public class ImportCategorySeedException : AbstractConsumersExceptions<ImportCategorySeedResponse>
{    public ImportCategorySeedException(ImportCategorySeedResponse response) : base(response) { }
}
