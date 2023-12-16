using Flunt.Notifications;

namespace Household.Budget.Contracts.Http.Responses;

public class Response<T> where T : class
{
    public Response() { }
    public Response(T data) => Data = data;
    public Response(IEnumerable<Notification> errors) => Errors = errors;
    public Response(Notification notification) => Errors = new List<Notification> { notification };
    public bool IsSuccess => Errors is null || Errors?.Count() == 0;
    public T? Data { get; set; }
    public IEnumerable<Notification>? Errors { get; set; }
}
