using Household.Budget.Contracts.ViewModels;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionRequest : Request
{
    public string Description { get; set; } = "";
    public CategoryViewModel Category { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public DateTime TransactionDate { get; set; }
    public List<string> Tags { get; set; } = [];

    public override void Validate() =>
        AddNotifications(new CreateTransactionRequestContract(this));
}