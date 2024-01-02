using System.Text.Json;

using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

namespace Household.Budget.Contracts.Exceptions;

public class AttachSubcategoryExecption : Exception
{
    public AttachSubcategoryExecption(SubcategoryWasCreatedEventResponse response)
        : base(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true })) { }
}
