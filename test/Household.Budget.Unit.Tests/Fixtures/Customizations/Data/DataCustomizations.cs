using System.Linq.Expressions;

using AutoFixture;

using Household.Budget.Contracts.Entities;
using Household.Budget.Domain.Data;

using NSubstitute;

namespace Household.Budget.Unit.Tests.Fixtures.Customizations.Data;

public class DataCustomizations<TEntity, TData>(Func<TData> data)
    : ICustomization where TEntity : Entity where TData : IData<TEntity>
{
    public TData Data { get; } = data();

    public void Customize(IFixture fixture)
    {
        Data.CreateAsync(Arg.Any<TEntity>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        Data.GetByIdAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<TEntity>());

        Data.UpdateAsync(Arg.Any<TEntity>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        Data.GetAllAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<PagedListResult<TEntity>>());

        Data.GetOneAsync(Arg.Any<Expression<Func<TEntity, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(fixture.Create<TEntity>());

        fixture.Register(() => Data);
    }
}
