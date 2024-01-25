using Household.Budget.Contracts.Enums;

namespace Household.Budget.Contracts.ViewModels;

public class PaymentViewModel
{
    public decimal Total { get; set; }
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
        if (Type != payment.Type && payment.Type is not PaymentType.CREDIT_CARD)
        {
            MergeWhenIsNotCreditCard(payment.Type);
        }
        if (payment.Type is PaymentType.CREDIT_CARD)
        {
            MergeWhenIsCreditCard(payment);
        }
    }

    private void MergeWhenIsNotCreditCard(PaymentType type)
    {
        Type = type;
        CreditCard = null;
    }

    private void MergeWhenIsCreditCard(PaymentViewModel payment)
    {
        Type = payment.Type;
        CreditCard ??= new();
        CreditCard.Merge(payment.CreditCard ?? new(), Total);
    }
}