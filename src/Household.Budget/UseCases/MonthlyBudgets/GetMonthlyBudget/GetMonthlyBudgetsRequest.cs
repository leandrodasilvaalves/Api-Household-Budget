using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Helpers;

namespace Household.Budget.UseCases.MonthlyBudgets.GetMonthlyBudget;

public class GetMonthlyBudgetsRequest : Request
{
    public int Year { get; set; }
    public Month Month { get; set; }

    public override void Validate()
    {   
        if(Contracts.Helpers.Year.IsValid(Year) is false)
            AddNotification(CommonErrors.INVALID_YEAR);        
    }
}
