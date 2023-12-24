using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdResponse : Response<Category>
{
    public GetCategoryByIdResponse(Category data) : base(data) { }
    public GetCategoryByIdResponse(Notification error) : base(error) { }
}