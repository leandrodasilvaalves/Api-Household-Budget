using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Categories.UpdateSubcategory;

public class UpdateSubcategoryResponse : Response<Subcategory>
{
    public UpdateSubcategoryResponse(Subcategory data) : base(data) { }
    public UpdateSubcategoryResponse(Notification error) : base(error) { }
}
