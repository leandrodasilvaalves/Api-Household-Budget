using Household.Budget.Contracts.Enums;

using MediatR;

namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedRequest(string name,
                                       CategoryType type,
                                       List<ImportSucategorySeedRequest> subCategories) : IRequest
{
    public string Name { get; } = name;
    public CategoryType Type { get; } = type;
    public List<ImportSucategorySeedRequest> SubCategories { get; } = subCategories;

    public string RootUserId { get; private set; } = "root";
    public ImportCategorySeedRequest WithRootUserId(string rootUserId)
    {
        RootUserId = rootUserId;
        return this;
    }
}

public class ImportSucategorySeedRequest(string name)
{
    public string Name { get; } = name;
}
