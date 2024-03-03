namespace Household.Budget.Domain.Models;

public class TotalModel
{
    public decimal Planned { get; set; } = default;
    public decimal Actual { get; set; } = default;
    public decimal Difference => Planned - Actual;
    public string Percentage => (Planned == 0 ? 0 : Actual / Planned * 100).ToString("P2");

    public void Calculate(IEnumerable<TotalModel> totals)
    {
        Planned = totals.Sum(x => x.Planned);
        Actual = totals.Sum(x => x.Actual);
    }

    public static explicit operator TotalModel(decimal? planned) =>
        new() { Planned = planned ?? 0, Actual = 0 };
}