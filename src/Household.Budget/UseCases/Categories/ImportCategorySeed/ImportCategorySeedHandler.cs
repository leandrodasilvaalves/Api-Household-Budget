using Household.Budget.Contracts.Constants;
using Household.Budget.Contracts.Enums;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

using MediatR;

namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedHandler : IRequestHandler<ImportCategorySeedRequest>
{
    private readonly IMediator _mediator;

    public ImportCategorySeedHandler(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task Handle(ImportCategorySeedRequest request, CancellationToken cancellationToken)
    {
        var createCategoryRequest = new CreateCategoryRequest(request.Name, ModelOwner.SYSTEM, request.Type)
        {
            UserId = request.RootUserId,
            UserClaims = [IdentityClaims.ADMIN_WRITER]
        };

        var category = await _mediator.Send(createCategoryRequest);
        request.SubCategories.ForEach(sub =>
        {
            if (Guid.TryParse(category.Data?.Id, out var categoryId))
            {
                _mediator.Send(new CreateSubcategoryRequest(sub.Name, categoryId));
            }
        });
    }
}