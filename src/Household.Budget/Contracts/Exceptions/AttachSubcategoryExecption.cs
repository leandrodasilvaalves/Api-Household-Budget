using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

namespace Household.Budget.Contracts.Exceptions;

public class AttachSubcategoryExecption : AbstractConsumersExceptions<SubcategoryWasCreatedEventResponse>
{
    public AttachSubcategoryExecption(SubcategoryWasCreatedEventResponse response) : base(response) { }
}
