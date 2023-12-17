using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.UseCases.Categories.GetCategoryById;

public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse>
{
    [FromRoute]
    public Guid Id { get; set; }
}
