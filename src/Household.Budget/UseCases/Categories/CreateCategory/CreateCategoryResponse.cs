using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.CreateCategories;

public class CreateCategoryResponse : AbstractResponse<Category>
{
    public CreateCategoryResponse(Category category) : base(category) { }
    public CreateCategoryResponse(IEnumerable<Notification> errors) : base(errors) { }
}