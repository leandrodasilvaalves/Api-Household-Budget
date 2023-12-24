using System.Net;

using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.GetCategoryById;
using Household.Budget.UseCases.Categories.ListCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Api.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] ListCategoriesRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NoContent);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] GetCategoryByIdRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.ToActionResult(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request)
    {
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var result = await _mediator.Send(request.WithId(id), HttpContext.RequestAborted);
        return result.ToActionResult(HttpStatusCode.OK);
    }
}