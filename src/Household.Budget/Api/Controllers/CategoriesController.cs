using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.GetCategoryById;
using Household.Budget.UseCases.Categories.ListCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] ListCategoriesRequest request,
                                                 [FromServices] IListCategoriesHandler handler)
    {
        var response = await handler.Handle(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NoContent);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] GetCategoryByIdRequest request,
                                                  [FromServices] IGetCategoryByIdHandler handler)
    {
        var response = await handler.Handle(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request,
                                                 [FromServices] ICreateCategoryHandler handler)
    {
        var result = await handler.Handle(request, HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request,
                                                 [FromServices] IUpdateCategoryHandler handler)
    {
        var result = await handler.Handle(request.WithId(id), HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.OK);
    }
}