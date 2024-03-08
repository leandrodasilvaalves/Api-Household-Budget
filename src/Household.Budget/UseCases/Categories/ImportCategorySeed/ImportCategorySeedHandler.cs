using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

using MassTransit;


namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedHandler : IImportCategorySeedHandler
{
    private readonly IBus _bus;
    private readonly ILogger<ImportCategorySeedHandler> _logger;
    private readonly ICreateCategoryHandler _createCategoryHandler;

    public ImportCategorySeedHandler(IBus bus, ILogger<ImportCategorySeedHandler> logger,
                                     ICreateCategoryHandler createCategoryHandler)
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _createCategoryHandler = createCategoryHandler ?? throw new ArgumentNullException(nameof(createCategoryHandler));
    }

    public async Task<ImportCategorySeedResponse> HandleAsync(ImportCategorySeedRequest request, CancellationToken cancellationToken)
    {
        var createCategoryRequest = new CreateCategoryRequest(request.Name, ModelOwner.SYSTEM, request.Type)
        {
            UserId = request.RootUserId,
            UserClaims = [IdentityClaims.ADMIN_WRITER]
        };

        var categoryResponse = await _createCategoryHandler.HandleAsync(createCategoryRequest, cancellationToken);
        if (categoryResponse.IsSuccess)
        {
            var sendEndpoint = await _bus.GetPublishSendEndpoint<CreateSubcategoryRequest>();

            request.SubCategories.ForEach(subcategory =>
                sendEndpoint.Send(
                    new CreateSubcategoryRequest(subcategory.Name, categoryResponse.Data?.Id),
                    ctx => ctx.Headers.Set(KnownHeaders.IMORT_PROCESS, true),
                    cancellationToken));

            _logger.LogInformation("Category has been imported: {0}", categoryResponse.Data?.Name);
            return new ImportCategorySeedResponse(request);
        }
        return new ImportCategorySeedResponse(categoryResponse?.Errors ?? []);
    }
}
