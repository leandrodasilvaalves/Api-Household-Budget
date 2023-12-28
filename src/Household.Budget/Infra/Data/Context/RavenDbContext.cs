using Household.Budget.Contracts.Data;

using Microsoft.Extensions.Options;

using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace Household.Budget.Infra.Data.Context;

public class RavenDbContext : IRavenDbContext, IDatabaseCreator
{
    private readonly RavenConfig _config;

    public RavenDbContext(IOptionsMonitor<RavenConfig> config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        _config = config.CurrentValue;

        Store = new DocumentStore
        {
            Urls = _config.Urls,
            Database = _config.DatabaseName
        };

        Store.Initialize();
    }

    public IDocumentStore Store { get; }

    public async Task EnsureDatabaseIsCreatedAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Store.Maintenance.ForDatabase(_config.DatabaseName)
                 .SendAsync(new GetStatisticsOperation(), cancellationToken);
        }
        catch
        {
            await Store.Maintenance.Server.SendAsync(
                new CreateDatabaseOperation(
                new DatabaseRecord(_config.DatabaseName)), cancellationToken);
        }
    }
}
