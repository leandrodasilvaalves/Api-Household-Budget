using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

namespace Household.Budget.Contracts.Exceptions;

public class AttachSubcategoryExecption : AbstractConsumersExceptions<AttachSubcategoryResponse>
{
    public AttachSubcategoryExecption(AttachSubcategoryResponse response) : base(response) { }
}
