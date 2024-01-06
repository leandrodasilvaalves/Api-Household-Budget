using System.Net;

using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.StatusCodeResults;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Contracts.Http.Responses;

public abstract class AbstractResponse<T> where T : class
{
    protected AbstractResponse(T data) => Data = data;
    protected AbstractResponse(IEnumerable<Notification> errors) => Errors = errors;
    protected AbstractResponse(Notification notification) => Errors = new List<Notification> { notification };
    public bool IsSuccess => (Errors is null || Errors?.Count() == 0) && Data is { };
    public T? Data { get; }
    public IEnumerable<Notification>? Errors { get; private set; }

    public IActionResult ToActionResult(params HttpStatusCode[] statusCodes) => IsSuccess switch
    {
        true when statusCodes.Contains(HttpStatusCode.OK) => new OkObjectResult(this),
        true when statusCodes.Contains(HttpStatusCode.Created) => new CustomCreatedResult(this),
        false when Data is null && statusCodes.Contains(HttpStatusCode.NoContent) => new CustomNoContentResult(NotContent()),
        false when Data is null && statusCodes.Contains(HttpStatusCode.NotFound) => new NotFoundObjectResult(NotFoundError()),
        false => new BadRequestObjectResult(this),
        _ => new InternalServerErrorResult(UnexpectedError())
    };

    public static Response UnexpectedError() => new(CommonErrors.UNEXPECTED_ERROR);
    protected virtual Response NotFoundError() => new(CommonErrors.NOT_FOUND);
    protected virtual Response NotContent() => new(CommonErrors.NO_CONTENT);
}