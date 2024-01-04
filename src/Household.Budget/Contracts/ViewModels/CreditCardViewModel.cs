namespace Household.Budget.Contracts.ViewModels;

public class CreditCardViewModel
{
    public string Name { get; set; } = "";
    public int? InstallmentNumber { get; set; }
    public DateTime? FirstDueDate { get; set; }
    public InstallMentViewModel? Installment { get; set; }

    public CreditCardViewModel ProcessPurchase(float totalPurchase)
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
}
