namespace Household.Budget.Contracts;



public static class CurrentYear
{
    public static int Current => DateTime.UtcNow.Year;
    public static int Min => 5;
    public static int Max => 40;

    public static bool IsValid(int year)
        => year >= Current - Min && year <= Current + Max;
}