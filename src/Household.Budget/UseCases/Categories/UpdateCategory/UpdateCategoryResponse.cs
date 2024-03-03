using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Categories.UpdateCategory;

public class UpdateCategoryResponse : AbstractResponse<Category>
{
    public UpdateCategoryResponse(Category data) : base(data) { }

    public UpdateCategoryResponse(Notification notification) 
        : base(notification) { }

    protected override Response NotFoundError() =>
        new(CategoryErrors.CATEGORY_NOT_FOUND);
}
