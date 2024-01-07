using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionResponse : AbstractResponse<Transaction>
{
    public UpdateTransactionResponse(Transaction data) : base(data) { }
    public UpdateTransactionResponse(IEnumerable<Notification> errors) : base(errors) { }
    public UpdateTransactionResponse(Notification notification) : base(notification) { }
}