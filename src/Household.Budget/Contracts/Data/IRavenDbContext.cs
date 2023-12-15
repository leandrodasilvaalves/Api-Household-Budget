using Raven.Client.Documents;

namespace Household.Budget.Contracts.Data;

public interface IRavenDbContext
{
    public IDocumentStore Store { get; }
}
