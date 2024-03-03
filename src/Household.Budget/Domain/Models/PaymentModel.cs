using Household.Budget.Contracts.Enums;

namespace Household.Budget.Domain.Models;

public class PaymentModel
{
    public decimal Total { get; set; }
    public PaymentType Type { get; set; }
    public CreditCardModel? CreditCard { get; set; }

    public PaymentModel Process()
    {
        if (Type == PaymentType.CREDIT_CARD)
        {
            CreditCard?.ProcessPurchase(Total);
        }
        return this;
    }

    public void Merge(PaymentModel payment)
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

    private void MergeWhenIsCreditCard(PaymentModel payment)
    {
        Type = payment.Type;
        CreditCard ??= new();
        CreditCard.Merge(payment.CreditCard ?? new(), Total);
    }
}