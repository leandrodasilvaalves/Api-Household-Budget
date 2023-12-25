using System.Net;

using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.StatusCodeResults;

using Microsoft.AspNetCore.Mvc;

namespace Household.Budget.Contracts.Http.Responses;

public class Response<T> where T : class
{
    public Response(T data) => Data = data;
    public Response(IEnumerable<Notification> errors) => Errors = errors;
    public Response(Notification notification) => Errors = new List<Notification> { notification };
    public bool IsSuccess => Errors is null || Errors?.Count() == 0;
    public T? Data { get; }
    public IEnumerable<Notification>? Errors { get; }

    public IActionResult ToActionResult(params HttpStatusCode[] statusCodes) => IsSuccess switch
    {
        true when statusCodes.Contains(HttpStatusCode.OK) => new OkObjectResult(this),
        true when statusCodes.Contains(HttpStatusCode.Created) => new CustomCreatedResult(this),
        true when Data is null && statusCodes.Contains(HttpStatusCode.NoContent) => new CustomNoContentResult(this),
        true when Data is null && statusCodes.Contains(HttpStatusCode.NotFound) => new NotFoundObjectResult(this),
        false => new BadRequestObjectResult(this),
        _ => new InternalServerErrorResult(UnexpectedError())
    };

    public static Response<T> UnexpectedError() => new(CommonErrors.UNEXPECTED_ERROR);
}
