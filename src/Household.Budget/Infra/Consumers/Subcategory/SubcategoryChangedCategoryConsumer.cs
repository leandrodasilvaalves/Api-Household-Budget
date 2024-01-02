using Household.Budget.Contracts.Events;
using Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;

using MassTransit;

namespace Household.Budget.Infra.Consumers;

public class SubcategoryChangedCategoryConsumer : IConsumer<SubcategoryChangedCategory>
{
    private readonly ISubcategoryChangeCategoryEventHandler _handler;

    public SubcategoryChangedCategoryConsumer(ISubcategoryChangeCategoryEventHandler handler) => 
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public Task Consume(ConsumeContext<SubcategoryChangedCategory> context) => 
        _handler.HandleAsync(context.Message, context.CancellationToken);
}