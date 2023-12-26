using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdRequest : Request, IRequest<GetSubcategoryByIdResponse>
{
    [FromRoute]
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }

    public override void Validate()
    {
        // throw new NotImplementedException();
    }
}
