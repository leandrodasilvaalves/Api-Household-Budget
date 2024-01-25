namespace Household.Budget.Contracts.ViewModels;

public class CreditCardViewModel
{
    public string Name { get; set; } = "";
    public int? InstallmentNumber { get; set; }
    public DateTime? FirstDueDate { get; set; }
    public InstallMentViewModel? Installment { get; set; }

    public CreditCardViewModel ProcessPurchase(decimal totalPurchase)
    {
        var installmentAmount = (totalPurchase / InstallmentNumber) ?? 0;

        Installment = new InstallMentViewModel(
            FirstDueDate ?? DateTime.Now,
            installmentAmount,
            InstallmentNumber);

        InstallmentNumber = null;
        FirstDueDate = null;
        return this;
    }

    public void Merge(CreditCardViewModel creditCard, decimal totalPurchase)
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