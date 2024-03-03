using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Models;

namespace Household.Budget.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionRequest : Request
{
    public string Id { get; set; } = "";
    public string? Description { get; set; }
    public CategoryModel? Category { get; set; }
    public PaymentModel? Payment { get; set; }
    public DateTime? TransactionDate { get; set; }
    public List<string>? Tags { get; set; }
    public ModelStatus? Status { get; set; }

    public override void Validate() =>
        AddNotifications(new UpdateTransactionRequestContract(this));
}