namespace Household.Budget.Infra.Data.Context;

public class MongoConfig
{
    public const string SectionName = "Mongo";
    public string? DatabaseName { get; set; }
    public string? ConnectionString { get; set; }
}