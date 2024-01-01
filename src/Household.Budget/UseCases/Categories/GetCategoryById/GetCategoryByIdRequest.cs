using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdRequest : Request
{
    [FromRoute]
    public Guid Id { get; set; }

    public override void Validate()
    {
        //TODO: corrigir. Está quebrando o  SPI
        // throw new NotImplementedException();
    }
}
