using Flunt.Notifications;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Domain.Entities;

namespace Household.Budget.UseCases.Transactions.GetTransactionById;

public class GetTransacationByIdResponse : AbstractResponse<Transaction>
{
    public GetTransacationByIdResponse(Transaction data) : base(data) { }
    public GetTransacationByIdResponse(Notification notification) : base(notification) { }
    public GetTransacationByIdResponse(IEnumerable<Notification> errors) : base(errors) { }

    protected override Response NotFoundError() =>
        new(TransactionErrors.TRANSACTION_NOT_FOUND);
}

