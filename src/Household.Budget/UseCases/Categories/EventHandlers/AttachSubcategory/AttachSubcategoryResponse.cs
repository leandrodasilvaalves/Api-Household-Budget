using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public class AttachSubcategoryResponse : AbstractResponse<Subcategory>
{
    public AttachSubcategoryResponse(Subcategory data) : base(data) { }
    public AttachSubcategoryResponse(Notification notification) : base(notification) { }
}