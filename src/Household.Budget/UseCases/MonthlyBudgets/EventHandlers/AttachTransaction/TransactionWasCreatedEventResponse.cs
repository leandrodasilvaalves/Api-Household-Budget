using Flunt.Notifications;

using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget.UseCases.MonthlyBudgets.EventHandlers.AttachTransaction;

public class TransactionWasCreatedEventResponse : AbstractResponse<TransactionWasCreated>
{
    public TransactionWasCreatedEventResponse(TransactionWasCreated data) : base(data) { }
    public TransactionWasCreatedEventResponse(Notification notification) : base(notification) { }
    public TransactionWasCreatedEventResponse(IEnumerable<Notification> errors) : base(errors) { }
}