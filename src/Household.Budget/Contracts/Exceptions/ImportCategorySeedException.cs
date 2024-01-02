using System.Text.Json;

using Household.Budget.UseCases.Categories.ImportCategorySeed;

namespace Household.Budget.Contracts.Exceptions;

public class ImportCategorySeedException : Exception
{
    public ImportCategorySeedException(ImportCategorySeedResponse response)
        : base(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true })) { }

}