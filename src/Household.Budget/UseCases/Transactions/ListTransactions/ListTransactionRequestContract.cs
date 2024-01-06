using Flunt.Validations;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Extensions;

namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionRequestContract : Contract<ListTransactionRequest>
{
    public ListTransactionRequestContract(ListTransactionRequest request)
    {
        var currentYear = DateTime.UtcNow.Year;
        Requires()
            .IsLowerOrEqualsThan(50, request.PageSize, CommonErrors.MAX_PAGE_SIZE)
            .IsBetween(request.Year, currentYear - 5, currentYear + 40, CommonErrors.INVALID_YEAR)
            .IsBetween(request.Month, 1, 12, CommonErrors.INVALID_MONTH);
    }
}