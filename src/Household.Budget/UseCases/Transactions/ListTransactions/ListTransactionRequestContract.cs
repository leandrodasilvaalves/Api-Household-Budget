using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;
using Household.Budget.Contracts.Helpers;

namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionRequestContract : Contract<ListTransactionRequest>
{
    public ListTransactionRequestContract(ListTransactionRequest request)
    {
        Requires()
            .IsLowerOrEqualsThan(50, request.PageSize, CommonErrors.MAX_PAGE_SIZE)
            .IsTrue(CurrentYear.IsValid(request.Year), CommonErrors.INVALID_YEAR)
            .IsBetween(request.Month, 1, 12, CommonErrors.INVALID_MONTH);
    }
}