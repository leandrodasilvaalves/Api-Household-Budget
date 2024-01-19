using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;
using Household.Budget.Contracts.ViewModels;

namespace Household.Budget.UseCases.Transactions.CreateTransaction;

public class CreateTransactionRequest : Request
{
    public string Description { get; set; } = "";
    public CategoryViewModel Category { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public DateTime TransactionDate { get; set; }
    public List<string> Tags { get; set; } = [];

    public Transaction ToModel(Category category, Subcategory subcategory) => new()
    {
        Id = $"{Guid.NewGuid()}",
        UserId = UserId,
        Description = Description,
        Category = CategoryViewModel.CreateFrom(category, subcategory),
        Payment = Payment.Process(),
        TransactionDate = TransactionDate,
        Tags = Tags,
        Status = ModelStatus.ACTIVE,
        Owner = ModelOwner.USER,
        Type = category.Type,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
    };

    public override void Validate() =>
        AddNotifications(new CreateTransactionRequestContract(this));
}