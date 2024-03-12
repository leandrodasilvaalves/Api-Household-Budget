namespace Household.Budget.Contracts.Helpers;

public static class Year
{
    public static int Current => DateTime.UtcNow.Year;
    public static int Min => 5;
    public static int Max => 40;

    public static bool IsValid(int year)
        => year >= Current - Min && year <= Current + Max;
}