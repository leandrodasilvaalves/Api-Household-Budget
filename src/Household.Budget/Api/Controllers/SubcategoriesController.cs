using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Categories.GetSubcategoryById;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;
using Household.Budget.UseCases.Subcategories.ListSubcategories;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers
{
    [ApiController]
    [Route("api/v1/subcategories")]
    public class SubcategoriesController : CustomControllerBase
    {   
        private readonly IMediator _mediator;

        public SubcategoriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] ListSubcategoriesRequest request)
        {
            var response = await _mediator.Send(request, HttpContext.RequestAborted);
            return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetSubcategoryByIdRequest request)
        {
            var response = await _mediator.Send(request, HttpContext.RequestAborted);
            return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSubcategoryRequest request)
        {
            var result = await _mediator.Send(request, HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.Created);
        }
    }
}