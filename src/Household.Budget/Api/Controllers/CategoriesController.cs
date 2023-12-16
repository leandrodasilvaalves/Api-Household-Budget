using Household.Budget.Contracts.Http.Controllers;
using Household.Budget.UseCases.CreateCategory;

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
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _mediator.Send(new ListCategoriesRequest(), HttpContext.RequestAborted);
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] GetCategoryByIdRequest request)
    {
        var category = await _mediator.Send(request, HttpContext.RequestAborted);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequest request)
    {
        var result = await _mediator.Send(request, HttpContext.RequestAborted);
        return result.IsSuccess ? Created(default(Uri), result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var result = await _mediator.Send(request.WithId(id), HttpContext.RequestAborted);
        return Ok(result);
    }
}