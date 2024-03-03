namespace Household.Budget.Domain.Models;

public class CreditCardModel
{
    public string Name { get; set; } = "";
    public int? InstallmentNumber { get; set; }
    public DateTime? FirstDueDate { get; set; }
    public InstallMentModel? Installment { get; set; }

    public CreditCardModel ProcessPurchase(decimal totalPurchase)
    {
        var installmentAmount = (totalPurchase / InstallmentNumber) ?? 0;

        Installment = new InstallMentModel(
            FirstDueDate ?? DateTime.Now,
            installmentAmount,
            InstallmentNumber);

        InstallmentNumber = null;
        FirstDueDate = null;
        return this;
    }

    public void Merge(CreditCardModel creditCard, decimal totalPurchase)
    {
        if (!string.IsNullOrWhiteSpace(creditCard.Name))
        {
            Name = creditCard.Name;
        }
        if(InstallmentNumber != creditCard?.InstallmentNumber)
        {
            InstallmentNumber = creditCard?.InstallmentNumber;
        }
        if(FirstDueDate != creditCard?.FirstDueDate)
        {
            FirstDueDate = creditCard?.FirstDueDate;
        }
        ProcessPurchase(totalPurchase);
    }
}