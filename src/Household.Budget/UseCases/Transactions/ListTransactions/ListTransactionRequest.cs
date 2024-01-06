namespace Household.Budget.UseCases.Transactions.ListTransactions;

public class ListTransactionRequest : Request
{
    public int Year { get; set; } = DateTime.Now.Year;
    public int Month { get; set; } = DateTime.Now.Month;
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;

    public override void Validate()
        => AddNotifications(new ListTransactionRequestContract(this));
}
