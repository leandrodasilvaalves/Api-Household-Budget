using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetSubcategoryById;

public class GetSubcategoryByIdRequest : Request, IRequest<GetSubcategoryByIdResponse>
{
    [FromRoute]
    public Guid Id { get; set; }

    public override void Validate()
    {
        // throw new NotImplementedException();
    }
}
