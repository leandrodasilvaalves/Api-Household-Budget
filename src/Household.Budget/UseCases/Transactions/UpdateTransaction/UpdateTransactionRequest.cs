using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.ViewModels;

namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionRequest : Request
{
    public string Id { get; set; } = "";
    public string? Description { get; set; }
    public CategoryViewModel? Category { get; set; }
    public PaymentViewModel? Payment { get; set; }
    public DateTime? TransactionDate { get; set; }
    public List<string>? Tags { get; set; }
    public ModelStatus? Status { get; set; }

    public override void Validate() =>
        AddNotifications(new UpdateTransactionRequestContract(this));
}