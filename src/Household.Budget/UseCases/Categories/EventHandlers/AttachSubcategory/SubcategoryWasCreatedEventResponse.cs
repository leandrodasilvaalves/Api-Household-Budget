using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;


namespace Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

public class SubcategoryWasCreatedEventResponse : AbstractResponse<Subcategory>
{
    public SubcategoryWasCreatedEventResponse(Subcategory data) : base(data) { }
    public SubcategoryWasCreatedEventResponse(Notification notification) : base(notification) { }
}