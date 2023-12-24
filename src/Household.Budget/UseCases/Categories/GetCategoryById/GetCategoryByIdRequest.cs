using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdRequest : Request, IRequest<GetCategoryByIdResponse>
{
    [FromRoute]
    public Guid Id { get; set; }

    public override void Validate()
    {
        // throw new NotImplementedException();
    }
}
