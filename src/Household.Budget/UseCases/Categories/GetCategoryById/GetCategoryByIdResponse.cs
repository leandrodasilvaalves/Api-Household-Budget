using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdResponse : AbstractResponse<Category>
{
    public GetCategoryByIdResponse(Category data) : base(data) { }
    public GetCategoryByIdResponse(Notification notification) : base(notification) { }
    public GetCategoryByIdResponse(IEnumerable<Notification> errors) : base(errors) { }

    protected override Response NotFoundError() =>
        new(CategoryErrors.CATEGORY_NOT_FOUND);
}