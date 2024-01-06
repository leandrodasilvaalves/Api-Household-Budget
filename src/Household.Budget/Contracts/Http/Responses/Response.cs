using Flunt.Notifications;

namespace Household.Budget.Contracts.Http.Responses;

public class Response : AbstractResponse<Request>
{
    public Response(Notification notification) : base(notification) { }

    public Response(IEnumerable<Notification> errors) : base(errors) { }
}
