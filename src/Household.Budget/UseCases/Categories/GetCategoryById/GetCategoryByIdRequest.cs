using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdRequest : Request
{
    [FromRoute]
    public string Id { get; set; }

    public override void Validate()
    {
        if(Id == default)
        {
            AddNotification("CATEGORY_ID_IS_REQUIRED", "Category Id is required");
        }
    }
}
