namespace Household.Budget.Domain.Models;

public class InstallMentModel
{
    public InstallMentModel() { }

    public InstallMentModel(DateTime firstDueDate, decimal amount, int? number)
    {
        Number = number ?? 0;
        if (Number > 0)
        {
            ProcessNextPayments(firstDueDate, amount);
        }
    }

    public int Number { get; set; }
    public List<NextPaymentModel> NextPayments { get; set; } = [];

    private void ProcessNextPayments(DateTime firstDueDate, decimal amount)
    {
        NextPayments = new List<NextPaymentModel>(Number);
        for (var i = 0; i < Number; i++)
        {
            NextPayments.Add(new NextPaymentModel
            {
                DueDate = firstDueDate.AddMonths(i),
                Amount = amount,
            });
        }
    }
}