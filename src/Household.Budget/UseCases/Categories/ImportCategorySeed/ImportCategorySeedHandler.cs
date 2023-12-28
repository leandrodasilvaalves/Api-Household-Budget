using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

using MediatR;

namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedHandler : IRequestHandler<ImportCategorySeedRequest>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ImportCategorySeedHandler> _logger;

    public ImportCategorySeedHandler(IMediator mediator, ILogger<ImportCategorySeedHandler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ImportCategorySeedRequest request, CancellationToken cancellationToken)
    {
        var createCategoryRequest = new CreateCategoryRequest(request.Name, ModelOwner.SYSTEM, request.Type)
        {
            UserId = request.RootUserId,
            UserClaims = [IdentityClaims.ADMIN_WRITER]
        };

        var category = await _mediator.Send(createCategoryRequest);

        var subcategoriesTasks = new List<Task>();
        request.SubCategories.ForEach(subcategory =>
        {
            if (Guid.TryParse(category.Data?.Id, out var categoryId))
            {
               subcategoriesTasks.Add(_mediator.Send(new CreateSubcategoryRequest(subcategory.Name, categoryId), cancellationToken));
            }
        });
        await Task.WhenAll(subcategoriesTasks);
        _logger.LogInformation("Category has been imported: {0}", category.Data?.Name);
    }
}