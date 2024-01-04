using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.ViewModels;

public class PaymentViewModel
{
    public float Total { get; set; }
    public PaymentType Type { get; set; }
    public CreditCardViewModel? CreditCard { get; set; }

    public PaymentViewModel Process()
    {
        if (Type == PaymentType.CREDIT_CARD)
        {
            CreditCard?.ProcessPurchase(Total);
        }
        return this;
    }
}
