using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget;

public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse>
{
    [FromRoute]
    public Guid Id { get; set; }
}
