using Household.Budget.Domain.Models;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionRequest : Request
{
    public string Description { get; set; } = "";
    public CategoryModel Category { get; set; } = new();
    public PaymentModel Payment { get; set; } = new();
    public DateTime TransactionDate { get; set; }
    public List<string> Tags { get; set; } = [];

    public override void Validate() =>
        AddNotifications(new CreateTransactionRequestContract(this));
}