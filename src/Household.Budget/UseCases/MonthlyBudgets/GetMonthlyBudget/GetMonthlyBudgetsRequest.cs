using Household.Budget.Contracts;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;

namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public class GetMonthlyBudgetsRequest : Request
{
    public int Year { get; set; }
    public Month Month { get; set; }

    public override void Validate()
    {   
        if(CurrentYear.IsValid(Year) is false)
            AddNotification(CommonErrors.INVALID_YEAR);        
    }
}
