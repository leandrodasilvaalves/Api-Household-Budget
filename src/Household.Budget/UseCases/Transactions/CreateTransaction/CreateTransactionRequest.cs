
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;
using Household.Budget.Contracts.ViewModels;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionRequest : Request
{
    public string Description { get; set; } = "";
    public CategoryViewModel Category { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public DateTime PurchaseDate { get; set; }
    public List<string> Tags { get; set; } = [];

    public Transaction ToModel() => new()
    {
        Id = $"{Guid.NewGuid()}",
        UserId = UserId,
        Description = Description,
        Category = Category,
        Payment = Payment.Process(),
        PurchaseDate = PurchaseDate,
        Tags = Tags,
        Status = ModelStatus.ACTIVE,
        Owner = ModelOwner.USER,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
    };

    public override void Validate()
    {
        // throw new NotImplementedException();
    }
}