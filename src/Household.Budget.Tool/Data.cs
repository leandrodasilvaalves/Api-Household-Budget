public class Data
{
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public float Jan { get; set; }
    public float Fev { get; set; }
    public float Mar { get; set; }
    public float Apr { get; set; }
    public float Mai { get; set; }
    public float Jun { get; set; }
    public float Jul { get; set; }
    public float Aug { get; set; }
    public float Sep { get; set; }
    public float Oct { get; set; }
    public float Nov { get; set; }
    public float Dec { get; set; }

    public float GetTotal(int month) => month switch
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
