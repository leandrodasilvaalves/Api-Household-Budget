using Household.Budget.Contracts.Errors;

namespace Household.Budget.Contracts.Http.Responses;

public class Response<T> where T : class
{
    public Response() { }
    public Response(T data) => Data = data;
    public Response(IEnumerable<Error> errors) => Errors = errors;
    public Response(Error error) => Errors = new List<Error> { error };
    public bool IsSuccess => Errors is null || Errors?.Count() == 0;
    public T? Data { get; set; }
    public IEnumerable<Error>? Errors { get; set; }
}
