using Household.Budget.Contracts.ViewModels;

namespace Household.Budget.Contracts.Models;

public class Transaction : Model
{
    public string Description { get; set; } = "";
    public CategoryViewModel Category { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public DateTime PurchaseDate { get; set; }
    public List<string> Tags { get; set; } = [];
}