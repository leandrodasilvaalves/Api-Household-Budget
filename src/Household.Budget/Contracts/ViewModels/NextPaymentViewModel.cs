namespace Household.Budget.Contracts.ViewModels;

public class NextPaymentViewModel
{
    public string? Id { get; set; }
    public float Amount { get; set; }
    public DateTime DueDate { get; set; }
}