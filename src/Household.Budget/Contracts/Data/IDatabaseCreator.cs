namespace Household.Budget.Contracts.Data;

public interface IDatabaseCreator
{
    Task EnsureDatabaseIsCreatedAsync(CancellationToken cancellationToken);
}
