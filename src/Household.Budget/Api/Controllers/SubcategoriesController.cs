using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Categories.GetSubcategoryById;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;
using Household.Budget.UseCases.Subcategories.ListSubcategories;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers
{
    [ApiController]
    [Route("api/v1/subcategories")]
    public class SubcategoriesController : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] ListSubcategoriesRequest request,
                                                     [FromServices] IListSubcategoriesHandler handler)
        {
            var response = await handler.Handle(request, HttpContext.RequestAborted);
            return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetSubcategoryByIdRequest request,
                                                      [FromServices] IGetSubcategoryByIdHandler handler)
        {
            var response = await handler.Handle(request, HttpContext.RequestAborted);
            return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSubcategoryRequest request,
                                                     [FromServices] ICreateSubcategoryHandler handler)
        {
            var result = await handler.Handle(request, HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.Created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateSubcategoryRequest request,
                                                     [FromServices] IUpdateSubcategoryHandler handler)
        {
            var result = await handler.Handle(request.WithId(id), HttpContext.RequestAborted);
            return result.ToActionResult(HttpStatusCode.OK);
        }
    }
}