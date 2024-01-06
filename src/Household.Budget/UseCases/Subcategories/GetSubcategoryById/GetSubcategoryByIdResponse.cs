using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdResponse : AbstractResponse<Subcategory>
{
    public GetSubcategoryByIdResponse(Subcategory data) : base(data) { }
    public GetSubcategoryByIdResponse(Notification notification) : base(notification) { }

    public GetSubcategoryByIdResponse(IEnumerable<Notification> errors) : base(errors) { }

    protected override Response NotFoundError() =>
        new (SubcategoryErrors.SUBCATEGORY_NOT_FOUND);
}