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
    private readonly ICreateSubcategoryHandler _createSubcategoryHandler;

    public ImportCategorySeedHandler(IBus bus, ILogger<ImportCategorySeedHandler> logger,
                                     ICreateCategoryHandler createCategoryHandler,
                                     ICreateSubcategoryHandler createSubcategoryHandler)
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _createCategoryHandler = createCategoryHandler ?? throw new ArgumentNullException(nameof(createCategoryHandler));
        _createSubcategoryHandler = createSubcategoryHandler ?? throw new ArgumentNullException(nameof(createSubcategoryHandler));
    }

    public async Task Handle(ImportCategorySeedRequest request, CancellationToken cancellationToken)
    {
        var createCategoryRequest = new CreateCategoryRequest(request.Name, ModelOwner.SYSTEM, request.Type)
        {
            UserId = request.RootUserId,
            UserClaims = [IdentityClaims.ADMIN_WRITER]
        };

        var categoryResponse = await _createCategoryHandler.Handle(createCategoryRequest, cancellationToken);
        if (categoryResponse.IsSuccess)
        {
            var categoryId = Guid.Parse(categoryResponse.Data?.Id ?? "");
            var subcategoriesTasks = new List<Task>();
            
            request.SubCategories.ForEach(subcategory =>
                subcategoriesTasks.Add(
                    _createSubcategoryHandler.Handle(
                        new CreateSubcategoryRequest(subcategory.Name, categoryId), cancellationToken)));

            await Task.WhenAll(subcategoriesTasks);
            _logger.LogInformation("Category has been imported: {0}", categoryResponse.Data?.Name);
        }
    }
}