using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdRequest : Request
{
    [FromRoute]
    public Guid Id { get; set; }

    public override void Validate()
    {
        if(Id == default)
        {
            AddNotification("SUBCATEGORY_ID_IS_REQUIRED", "Subcategory Id is required");
        }
    }
}
