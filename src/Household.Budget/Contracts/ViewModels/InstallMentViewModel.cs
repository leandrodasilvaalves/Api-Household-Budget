namespace Household.Budget.Contracts.ViewModels;

public class InstallMentViewModel
{
    public InstallMentViewModel() { }
    
    public InstallMentViewModel(DateTime firstDueDate, float amount, int? number)
    {
        Number = number ?? 0;
        if (Number > 0)
        {
            ProcessNextPayments(firstDueDate, amount);
        }
    }

    public int Number { get; set; }
    public List<NextPaymentViewModel> NextPayments { get; set; } = [];

    private void ProcessNextPayments(DateTime firstDueDate, float amount)
    {
        NextPayments = new List<NextPaymentViewModel>(Number);
        for (var i = 0; i < Number; i++)
        {
            NextPayments.Add(new NextPaymentViewModel
            {
                DueDate = firstDueDate.AddDays(30 * i),
                Amount = amount,
            });
        }
    }
}