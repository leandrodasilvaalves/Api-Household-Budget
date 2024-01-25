public class Data
{
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public decimal Jan { get; set; }
    public decimal Fev { get; set; }
    public decimal Mar { get; set; }
    public decimal Apr { get; set; }
    public decimal Mai { get; set; }
    public decimal Jun { get; set; }
    public decimal Jul { get; set; }
    public decimal Aug { get; set; }
    public decimal Sep { get; set; }
    public decimal Oct { get; set; }
    public decimal Nov { get; set; }
    public decimal Dec { get; set; }

    public decimal GetTotal(int month) => month switch
    {
        1 => Jan,
        2 => Fev,
        3 => Mar,
        4 => Apr,
        5 => Mai,
        6 => Jun,
        7 => Jul,
        8 => Aug,
        9 => Sep,
        10 => Oct,
        11 => Nov,
        12 => Dec,
        _ => throw new ArgumentOutOfRangeException(),
    };
}
