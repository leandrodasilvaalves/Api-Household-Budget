using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.CreateCategories;

using MassTransit;


namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedHandler : IImportCategorySeedHandler
{
    private readonly IBus _bus;
    private readonly ILogger<ImportCategorySeedHandler> _logger;

    public ImportCategorySeedHandler(IBus bus, ILogger<ImportCategorySeedHandler> logger)
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ImportCategorySeedRequest request, CancellationToken cancellationToken)
    {
        var createCategoryRequest = new CreateCategoryRequest(request.Name, ModelOwner.SYSTEM, request.Type)
        {
            UserId = request.RootUserId,
            UserClaims = [IdentityClaims.ADMIN_WRITER]
        };

        // var categoryResponse = await _bus.Send(createCategoryRequest);
        // if (categoryResponse.IsSuccess)
        // {
        //     var categoryId = Guid.Parse(categoryResponse.Data?.Id ?? "");
        //     var subcategoriesTasks = new List<Task>();
        //     request.SubCategories.ForEach(subcategory =>
        //         subcategoriesTasks.Add(_bus.Send(
        //             new CreateSubcategoryRequest(subcategory.Name, categoryId), cancellationToken)));

        //     await Task.WhenAll(subcategoriesTasks);
        //     _logger.LogInformation("Category has been imported: {0}", categoryResponse.Data?.Name);
        // }
    }
}