using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class SubcategoriesController : CustomControllerBase
    {   
        private readonly IMediator _mediator;

        public SubcategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("{id:guid}/subcategories")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid id, [FromBody] CreateSubcategoryRequest request)
        {
            var result = await _mediator.Send(request.WithCategoryId(id), HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.Created);
        }
    }
}