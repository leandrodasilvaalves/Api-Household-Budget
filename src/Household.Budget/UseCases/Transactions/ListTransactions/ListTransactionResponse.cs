
using Flunt.Notifications;

using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionResponse : AbstractResponse<PagedListResult<Transaction>>
{
    public ListTransactionResponse(PagedListResult<Transaction> data) : base(data) { }
    public ListTransactionResponse(IEnumerable<Notification> errors) : base(errors) { }
    public ListTransactionResponse(Notification notification) : base(notification) { }
}