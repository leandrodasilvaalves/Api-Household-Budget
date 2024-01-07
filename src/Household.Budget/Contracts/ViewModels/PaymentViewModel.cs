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

    public void Merge(PaymentViewModel payment)
    {
        if (Total != payment.Total)
        {
            Total = payment.Total;
        }
        if (Type != payment.Type)
        {
            Type = payment.Type;
            Merge(payment.CreditCard);
        }
    }

    private void Merge(CreditCardViewModel? creditCard)
    {
        if (Type == PaymentType.CREDIT_CARD)
        {
            CreditCard ??= new();
            CreditCard.Merge(creditCard ?? new(), Total);
        }
        else
        {
            CreditCard = null;
        }
    }
}