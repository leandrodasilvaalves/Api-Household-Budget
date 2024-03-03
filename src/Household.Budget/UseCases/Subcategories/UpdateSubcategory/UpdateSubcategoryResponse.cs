using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryResponse : AbstractResponse<Subcategory>
{
    public UpdateSubcategoryResponse(Subcategory data) : base(data) { }
    public UpdateSubcategoryResponse(Notification error) : base(error) { }

    protected override Response NotFoundError() =>
        new(SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
}
