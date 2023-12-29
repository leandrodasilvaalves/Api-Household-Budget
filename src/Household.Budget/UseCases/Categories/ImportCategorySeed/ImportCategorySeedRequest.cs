using Household.Budget.Contracts.Enums;

using MediatR;

namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedRequest : IRequest
{
    public string Name { get; set; } = "";
    public CategoryType Type { get; set; } = default;
    public List<ImportSucategorySeedRequest> SubCategories { get; set; } = [];

    public string RootUserId { get; set; } = "";
    public ImportCategorySeedRequest WithRootUserId(string rootUserId)
    {
        RootUserId = rootUserId;
        return this;
    }
}

public class ImportSucategorySeedRequest
{
    public string Name { get; set; } = "";
}
