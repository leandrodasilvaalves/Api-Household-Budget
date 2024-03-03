
using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionResponse : AbstractResponse<Transaction>
{
    public CreateTransactionResponse(Transaction data) : base(data) { }

    public CreateTransactionResponse(Notification notification) : base(notification) { }

    public CreateTransactionResponse(IEnumerable<Notification> errors) : base(errors) { }
}