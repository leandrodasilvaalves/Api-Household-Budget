namespace Household.Budget.Infra.Data.Context
{
  public class RavenConfig
  {
    public const string SectionName = "RavenDb";
    public string? DatabaseName { get; set; }
    public string[]? Urls { get; set; }
  }
}