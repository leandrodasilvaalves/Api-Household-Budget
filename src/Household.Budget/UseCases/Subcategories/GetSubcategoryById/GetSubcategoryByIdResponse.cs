using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdResponse : Response<Subcategory>
{
    public GetSubcategoryByIdResponse(Subcategory data) : base(data) { }
    public GetSubcategoryByIdResponse(Notification error) : base(error) { }
}