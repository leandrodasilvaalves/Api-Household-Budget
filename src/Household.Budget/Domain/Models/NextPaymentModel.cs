namespace Household.Budget.Domain.Models;

public class NextPaymentModel
{
    public string? Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}